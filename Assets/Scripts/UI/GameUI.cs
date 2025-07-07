using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    [SerializeField] ProjectileLauncher projectileLauncher;

    [SerializeField] UIDocument myUI;

    VisualElement root;

    Button throwBtn;

    Slider angle;
    Slider mass;
    Slider speed;

    private void Awake()
    {
        root = myUI.rootVisualElement;

        throwBtn = root.Q<Button>("ThrowBtn");

        angle = root.Q<Slider>("Angle");
        mass = root.Q<Slider>("Mess");
        speed = root.Q<Slider>("Speed");

        throwBtn.clicked += OnThrowButtonClick;
        angle.value = projectileLauncher.launchPoint.transform.rotation.z;

        angle.RegisterValueChangedCallback(evt =>
        {
            float angle = evt.newValue;
            projectileLauncher.launchPoint.transform.rotation = Quaternion.Euler(0, 0, angle);
        });
    }

    private void OnThrowButtonClick()
    {
        projectileLauncher.ThrowStone();
        //throwBtn.SetEnabled(false);
    }

    void OnThrowButtonClear()
    {
        throwBtn.SetEnabled(true);
    }

}
