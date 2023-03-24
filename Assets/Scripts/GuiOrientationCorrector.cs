using UnityEngine;
using UnityEngine.UI;

namespace Clock
{
    public class GuiOrientationCorrector : MonoBehaviour
    {
        [SerializeField] private LayoutGroupConfig _horizontalConfig;
        [SerializeField] private LayoutGroupConfig _verticalConfig;

        private GameObject _guiObject;
        private HorizontalOrVerticalLayoutGroup _currentLayoutGroup;

        private ITimeService _timeService;
        private ScreenOrientation _lastOrientation;

        private void Awake()
        {
            _timeService = ServiceLocator.GetService<ITimeService>();
            _guiObject = FindObjectOfType<Canvas>().gameObject;
            _lastOrientation = Screen.orientation;

            HandleScreenOrientationChange();
        }

        private void OnEnable() => _timeService.OnTimeUpdated += HandleTimeUpdate;

        private void OnDisable() => _timeService.OnTimeUpdated -= HandleTimeUpdate;

        // Проблема: во время настройки будильника событие не вызывается.
        private void HandleTimeUpdate()
        {
            if (_lastOrientation == Screen.orientation)
                return;

            _lastOrientation = Screen.orientation;
            HandleScreenOrientationChange();
        }

        private void HandleScreenOrientationChange()
        {
            if (_currentLayoutGroup != null)
                DestroyImmediate(_guiObject.GetComponent<LayoutGroup>());

            if (_lastOrientation == ScreenOrientation.LandscapeLeft || _lastOrientation == ScreenOrientation.LandscapeRight)
            {
                _currentLayoutGroup = _guiObject.AddComponent<HorizontalLayoutGroup>();
                ConfigureLayoutGroup(_horizontalConfig);
            }
            else if (_lastOrientation == ScreenOrientation.Portrait || _lastOrientation == ScreenOrientation.PortraitUpsideDown)
            {
                _currentLayoutGroup = _guiObject.AddComponent<VerticalLayoutGroup>();
                ConfigureLayoutGroup(_verticalConfig);
            }           
        }

        private void ConfigureLayoutGroup(LayoutGroupConfig config) 
        {
            _currentLayoutGroup.childAlignment = config.ChildAlignment;
            _currentLayoutGroup.padding.bottom = config.Bottom;
            _currentLayoutGroup.spacing = config.Spacing;
        }
    }
}