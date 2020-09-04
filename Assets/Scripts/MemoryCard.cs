using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour //Делаем переворот карты по щелчку
{
    [SerializeField] private GameObject cardBack;
    public void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canReveal)
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this); //Уведомляем контроллер об открытии карты
        }
    }

    public void Unreveal() //метод для закрытия карты
    {
        cardBack.SetActive(true);
    }

    [SerializeField] private SceneController controller; //часть кода для замены картинки

    private int _id;
    public int id
    {
        get { return _id; } //функуия чтения
    }

    public void SetCard(int id, Sprite image) //открытый метод для передачи спрайтов
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image; //сопоставляемый спрайт
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
