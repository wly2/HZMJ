using UnityEngine;
using UnityEngine.UI;
/*
 * 单人投票结果
*/
namespace AssemblyCSharp
{
    public class PlayerResult : MonoBehaviour
    {
        public new Text name;
        public Text result;

        public PlayerResult()
        {
        }

        public void SetInitVal(string namestr, string resultstr)
        {
            name.text = namestr;
            result.text = resultstr;
        }
    }
}