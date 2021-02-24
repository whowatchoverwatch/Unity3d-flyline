using System;
using UnityEngine;
using UnityEngine.UI;
namespace AndrewBox.Util
{
    public class UIConsole : MonoBehaviour
    {
        private static string M_allInfor="";
        private static Text M_TxtContainer;
        [SerializeField][Tooltip("文本框")]
        public Text m_text;

	    // Update is called once per frame
	    void Update ()
	    {
            if (m_text != null && !m_text.text.Equals(M_allInfor))
            {
                m_text.text = M_allInfor;
            }
	    }

        public static string AllInfor
        {
            get
            {
                return M_allInfor;
            }
        }
        public static void Log(string infor)
        {
            M_allInfor += infor + "\n";

        }

    }
}
