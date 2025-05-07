using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lando;
using TMPro;
using System;

public class TestNFC : MonoBehaviour
{
    [Serializable]
    struct CustomCard
    {
        public string Id;
        public string Info;
    
    }

    Cardreader reader;
    [SerializeField] TextMeshProUGUI textComp;
    [SerializeField] TextMeshProUGUI textInfoCard;

    [Header("Cards")]

    [SerializeField] List<CustomCard> Cards;

    void OnEnable()
    {
        reader = new Cardreader();
        reader.StartWatch();
        reader.CardConnected += CardReader_CardConnected;
        reader.CardDisconnected += CardReader_CardDisconnected;
    }

    private void CardReader_CardConnected(object sender, CardreaderEventArgs e)
    {
        Debug.Log(e.Card.Id);
        textComp.text = e.Card.Id;
        ReadNFC(e.Card.Id);
    }

    private void CardReader_CardDisconnected(object sender, CardreaderEventArgs e)
    {
        textComp.text = "...";
        textInfoCard.text = "...";
    }

    private void OnDisable()
    {
        reader.StopWatch();
        reader.Dispose();
    }

    public void ReadNFC(string id)
    {
        var card = Cards.Find(c => id == c.Id);
        if (card.Id != null)
            textInfoCard.text = card.Info;
        else
            textInfoCard.text = "Tarjeta no reconocida";
    }

}
