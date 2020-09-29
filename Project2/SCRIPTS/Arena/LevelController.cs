using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelController : MonoBehaviour
{
    [SerializeField]
    private DiabloController _diabloController;
    private Generator _generatorController;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Color mediumColor;
    [SerializeField]
    private Color hardColor;

    public static GameObject Canvas;
    public GameObject _canvas2;
  

    public  enum eLevels
    {
        Easy,
        Medium,
        Hard,
        Hell
    }

    public eLevels level;

    public Color MediumColor { get => mediumColor; set => mediumColor = value; }
    public Color HardColor { get => hardColor; set => hardColor = value; }

    void Awake()
    {
       
       
            
        Canvas = _canvas2;
        _generatorController = GetComponent<Generator>();
        level = eLevels.Easy; // Изначально стоит изи лвл
    }

    void Start()
    {
     
    }

    
    public void ToEasyLevel()
    {
        _generatorController.y = Random.Range(-10,11);
        _generatorController.z = Random.Range(10, 20); 
    }

    public void ToMediumLevel()
    {

    }

    public void ChangeFOGcolor(Color color)
    {
        
     
        _camera.backgroundColor = Color.Lerp(_camera.backgroundColor, color, 2 * Time.deltaTime);
        RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, color, 2 * Time.deltaTime);
    }
}
