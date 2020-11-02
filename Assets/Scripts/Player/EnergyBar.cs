using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class EnergyBar : MonoBehaviour
    {
        public Slider slider;
        public Gradient gradient;
        public Image fill;

        public void SetMaxEnergy(int maxEnergy)
        {
            slider.maxValue = maxEnergy;
            slider.value = maxEnergy;
            fill.color =  gradient.Evaluate(1f);
        }

        public void SetEnergy(float energy)
        {
            slider.value = energy;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}
