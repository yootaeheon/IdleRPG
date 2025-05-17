using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.View
{
    public class UIInventoryDescription : MonoBehaviour
    {
        [SerializeField] Image _itemImage;

        [SerializeField] TMP_Text _title;

        [SerializeField] TMP_Text _description;

        private void Awake()
        {
            ResetDescription();
        }

        /// <summary>
        /// ������ ������ ������ �ʱ�ȭ
        /// </summary>
        public void ResetDescription()
        {
            _itemImage.gameObject.SetActive(false);
            _title.text = "";
            _description.text = "";
        }

        /// <summary>
        /// ������ ��������Ʈ, �̸�, ���� �ʱ�ȭ
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="itemName"></param>
        /// <param name="itemDescription"></param>
        public void SetDescription(Sprite sprite, string itemName, string itemDescription)
        {
            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = sprite;
            _title.text = itemName;
            _description.text = itemDescription;
        }
    }
}