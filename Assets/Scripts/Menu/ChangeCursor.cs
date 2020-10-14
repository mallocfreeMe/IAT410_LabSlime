using UnityEngine;

namespace Menu
{
    public class ChangeCursor : MonoBehaviour
    {

        public Texture2D cursorArrow;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
