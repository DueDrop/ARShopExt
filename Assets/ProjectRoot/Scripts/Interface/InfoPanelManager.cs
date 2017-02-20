using UnityEngine;
using UnityEngine.UI;
using InfoServerObjectModel;

public class InfoPanelManager : MonoBehaviour {

    public RectTransform InfoPanel;
    public Text InfoText;
    public Text PriceQuantityText;
    public Image ItemImage;
	
    public void Start()
    {
        InfoPanel.gameObject.SetActive(false);
    }

    public void ShowPanel(InfoServerResponse<InfoServerMarkerResponse> info) {

        InfoText.text = info.data.description;
        string priceQuantity = @"<b>Цена</b> {0} р.
<b>Остаток</b> {1} шт.";
        PriceQuantityText.text = string.Format(priceQuantity, info.data.price, info.data.quantity);

        if (ItemImage.sprite != null) Destroy(ItemImage.sprite);

        if (!string.IsNullOrEmpty(info.data.image)) {

            Texture2D tex2d = new Texture2D(ItemImage.mainTexture.width, ItemImage.mainTexture.height);
            tex2d.LoadImage(System.Convert.FromBase64String(info.data.image));

            ItemImage.sprite = Sprite.Create(tex2d, new Rect(0,0, tex2d.width, tex2d.height), new Vector2(0.5f, 0.5f));
        }

        InfoPanel.gameObject.SetActive(true);

    }

    public void  HidePanel() {

        InfoPanel.gameObject.SetActive(false);

    }

}
