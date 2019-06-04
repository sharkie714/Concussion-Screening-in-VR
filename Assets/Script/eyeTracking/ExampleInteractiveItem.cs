using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;
namespace VRStandardAssets.Examples
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class ExampleInteractiveItem : MonoBehaviour
    {
        [SerializeField] private Material m_NormalMaterial;                
        [SerializeField] private Material m_OverMaterial;                  
        [SerializeField] private Material m_ClickedMaterial;                      
        [SerializeField] private VRInteractiveItem m_InteractiveItem;
        [SerializeField] private Renderer m_Renderer;

        Scene m_scene;
        private void Awake ()
        {
            m_Renderer.material = m_NormalMaterial;
        }


        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
            m_InteractiveItem.OnClick += HandleClick;
        }


        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
            m_InteractiveItem.OnClick -= HandleClick;
        }


        //Handle the Over event
        private void HandleOver()
        {
            Debug.Log("Show over state");
            m_Renderer.material = m_OverMaterial;
        }


        //Handle the Out event
        private void HandleOut()
        {
            Debug.Log("Show out state");
            m_Renderer.material = m_NormalMaterial;
        }


        //Handle the Click event
        private void HandleClick()
        {
            Debug.Log("Show click state");
            m_Renderer.material = m_ClickedMaterial;
            m_scene = SceneManager.GetActiveScene();
            if(m_scene.name == "Menu")
            {
                SceneManager.LoadScene("Scene1");
            }
            else if(m_scene.name == "Scene1")
            {
                SceneManager.LoadScene("Scene1");
            }
            else if (m_scene.name == "Scene2")
            {
                SceneManager.LoadScene("Scene3");
            }
            else if (m_scene.name == "Scene3")
            {
                SceneManager.LoadScene("Scene4");
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }

        }
    }

}