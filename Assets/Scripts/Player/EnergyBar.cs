using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class EnergyBar : MonoBehaviour
    {
        public Slider slider;
        public Gradient gradient;
        public Image fill;
        private PlayerSkill colors;

        public void SetMaxEnergy(int maxEnergy)
        {
            slider.maxValue = maxEnergy;
            slider.value = maxEnergy;
            fill.color =  gradient.Evaluate(1f);
            //fill.color = new Color(colors.rVal, colors.gVal, colors.bVal, 255);
            //fill.color = new Color(1, 0, 0, 1);
        }

        public void SetEnergy(float energy)
        {
            slider.value = energy;

            fill.color = gradient.Evaluate(slider.normalizedValue);
            //slider.color = new Color(colors.rVal, colors.gVal, colors.bVal, 255);
        }
    }
}
