using CalculatorLab12.Models;
using CalculatorLab12.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CalculatorLab12.ViewModels
{
    public class ViewModelOperaciones : ViewModelBase
    {
        private string resultText;
        public string ResultText
        {
            get { return resultText; }
            set
            {
                if (resultText != value)
                {
                    resultText = value;
                    OnPropertyChanged(); // Notifica el cambio de la propiedad a la vista
                }
            }
        }

        private double firstNumber;
        private double secondNumber;
        private string mathOperator;
        private int currentState;

        public ViewModelOperaciones()
        {
            OnClear();
        }

        public void OnSelectNumber(string pressed)
        {
            if (ResultText == "0" || currentState < 0)
            {
                ResultText = "";
                if (currentState < 0)
                    currentState *= -1;
            }

            ResultText += pressed;

            double number;
            if (double.TryParse(ResultText, out number))
            {
                ResultText = number.ToString("N0");
                if (currentState == 1)
                {
                    firstNumber = number;
                }
                else
                {
                    secondNumber = number;
                }
            }
        }

        public void OnSelectOperator(string pressed)
        {
            currentState = -2;
            mathOperator = pressed;
        }

        public void OnClear()
        {
            firstNumber = 0;
            secondNumber = 0;
            currentState = 1;
            ResultText = "0";
        }

        public void OnCalculate()
        {
            if (currentState == 2)
            {
                var result = SimpleCalculator.Calculate(firstNumber, secondNumber, mathOperator);
                ResultText = result.ToString();
                firstNumber = result;
                currentState = -1;
            }
        }
    }
}
