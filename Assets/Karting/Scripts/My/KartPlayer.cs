using UnityEngine;
//using UnityEngine.SceneManagement;

public class KartPlayer : MonoBehaviour
{
    GameFlowManager m_GameFlowManager;

    void Awake()
    {
        m_GameFlowManager = FindObjectOfType<GameFlowManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            //SceneManager.LoadScene("LoseScene");
            m_GameFlowManager.EndGame(false);
    }
}
