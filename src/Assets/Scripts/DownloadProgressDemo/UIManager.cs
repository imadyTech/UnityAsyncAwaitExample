using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace imady.Common
{
    public class UIManager : MonoBehaviour
    {
        private static Text _promptTextBox;
        private static string _promtText;

        private void OnEnable()
        {
            _promptTextBox = GameObject.Find("TuiCanvas/PromptTextBox").GetComponent<Text>();
            _promptTextBox.gameObject.SetActive(true);
        }
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }


        public static void ShowPrompt(string prompt)
        {
            _promtText = prompt;
            _promptTextBox.gameObject.SetActive(true);
            _promptTextBox.text = _promtText;
        }
        public static void HidePrompt()
        {
            _promptTextBox.gameObject.SetActive(false);
        }
    }
}