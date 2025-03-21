using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Services.AssetPreloader;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader : ISceneLoader
    {
        private const float MinDisplayedProgress = 0.2f;

        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IAssetPreloader _assetPreloader;

        public SceneLoader(ICoroutineRunner coroutineRunner, IAssetPreloader assetPreloader)
        {
            _assetPreloader = assetPreloader;
            _coroutineRunner = coroutineRunner;
        }

        public async UniTask LoadForce(string name, Action onLevelLoad, ILoadingCurtain loadingCurtain)
        {
            await LoadLevelAsync(name, onLevelLoad, loadingCurtain, loadForce: true, isAddressable: true);
        }

        public async UniTask Load(string name, Action onLevelLoad, bool isAddressable, ILoadingCurtain loadingCurtain)
        {
            await LoadLevelAsync(name, onLevelLoad, loadingCurtain, loadForce: false, isAddressable: isAddressable);
        }

        private async UniTask LoadLevelAsync(string name, Action onLevelLoad, ILoadingCurtain loadingCurtain,
            bool loadForce, bool isAddressable)
        {
            if (!loadForce && SceneManager.GetActiveScene().name == name)
            {
                onLevelLoad?.Invoke();
                return;
            }

            AsyncOperationStatus asyncOperationStatus = AsyncOperationStatus.None;
            if (isAddressable && await _assetPreloader.NeedLoadAssetsFor(name))
            {
                Debug.LogError($"Content not loaded for {name}");
                Coroutine fakeLoadingCurtain = _coroutineRunner.StartCoroutine(FakeLoadingBar(loadingCurtain));
                asyncOperationStatus = await _assetPreloader.LoadAssetsFor(name);
                _coroutineRunner.StopCoroutine(fakeLoadingCurtain);
            }

            if (asyncOperationStatus == AsyncOperationStatus.Failed)
            {
                loadingCurtain?.ShowNoInternetWarning(() => LoadForce(name, onLevelLoad, loadingCurtain));
                return;
            }

            _coroutineRunner.StartCoroutine(LoadLevel(name, onLevelLoad, loadingCurtain, isAddressable));
        }

        private IEnumerator LoadLevel(string name, Action onLevelLoad, ILoadingCurtain loadingCurtain, bool isAddressable)
        {
            if (isAddressable)
            {
                AsyncOperationHandle<SceneInstance> asyncOperationHandle = Addressables.LoadSceneAsync(name);
        
                while (!asyncOperationHandle.IsDone)
                {
                    float progress = asyncOperationHandle.PercentComplete;
                    loadingCurtain?.ShowProgress(Mathf.Max(MinDisplayedProgress, progress));
                    yield return null;
                }

                if (!asyncOperationHandle.Result.Scene.IsValid())
                {
                    loadingCurtain?.ShowNoInternetWarning(() => LoadForce(name, onLevelLoad, loadingCurtain));
                    yield break;
                }
            }
            else
            {
                AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(name);
                while (!loadSceneOperation.isDone)
                {
                    float progress = loadSceneOperation.progress;
                    loadingCurtain?.ShowProgress(Mathf.Max(MinDisplayedProgress, progress));
                    yield return null;
                }
            }
            
            loadingCurtain?.ShowProgress(1);
            onLevelLoad?.Invoke();
        }

        private IEnumerator FakeLoadingBar(ILoadingCurtain loadingCurtain)
        {
            float progress = MinDisplayedProgress;
            while (true)
            {
                loadingCurtain?.ShowProgress(progress);
                progress += progress < 0.5f
                    ? Time.deltaTime * 0.02f
                    : Time.deltaTime * 0.01f;

                progress = Mathf.Clamp01(progress);

                if (progress > 0.95f)
                    progress = 0.46f;

                yield return null;
            }
        }
    }
}