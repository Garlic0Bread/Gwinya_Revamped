using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when something get into the alta, make the runes glow
namespace Cainos.PixelArtTopDown_Basic
{

    public class PropsAltar : MonoBehaviour
    {
        [SerializeField] private int levelToLoad;
        [SerializeField] private GameObject fire;
        public List<SpriteRenderer> runes;
        public float lerpSpeed;

        private Color curColor;
        private Color targetColor;

        private void OnTriggerEnter2D(Collider2D other)
        {
            targetColor = new Color(1, 1, 1, 1);
            if (this.gameObject.CompareTag("StartPoint"))
            {
                GameCurrency gc = GameObject.FindObjectOfType<GameCurrency>();
                gc.EnableStore(true);
            }
            
            else if (this.gameObject.CompareTag("EndPoint"))
            {
                Menu_Ctrl nextLevel = FindObjectOfType<Menu_Ctrl>();
                fire.SetActive(true);
                nextLevel.LoadNextLevel(levelToLoad);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            targetColor = new Color(1, 1, 1, 0);

            GameCurrency gc = GameObject.FindObjectOfType<GameCurrency>();
            gc.EnableStore(false);
        }

        private void Update()
        {
            curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

            foreach (var r in runes)
            {
                r.color = curColor;
            }
        }
    }
}
