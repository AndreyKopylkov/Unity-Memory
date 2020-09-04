using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2; //значения для сетки
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    [SerializeField] private MemoryCard originalCard; //ссылка на краты в сцене
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMesh scoreLabel;

    private MemoryCard _firstRevelead;
    private MemoryCard _secondReveled;

    private int _score = 0;

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevelead == null)
        {
            _firstRevelead = card;
        } else
        {
            _secondReveled = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevelead.id == _secondReveled.id)
        {
            _score++; //увеличивает счёт на 1 при совпадении
            scoreLabel.text = "Score: " + _score;
        }
        else
        {
            yield return new WaitForSeconds(.5f);

            _firstRevelead.Unreveal(); //Закрываем несовпадающие карты
            _secondReveled.Unreveal();
        }

        _firstRevelead = null; //очищаем переменные
        _secondReveled = null;
    }

    public bool canReveal
    {
        get {return _secondReveled == null;} //возвращает false, если вторая карьа уже октрыта
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = originalCard.transform.position; //положение первой карты

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j <gridRows; j++)
            {
                MemoryCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }

        
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
