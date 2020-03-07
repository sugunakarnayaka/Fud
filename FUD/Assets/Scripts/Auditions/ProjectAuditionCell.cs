﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ProjectAuditionCell : MonoBehaviour
{
    public Image icon;
    public TMP_Text titleText;
    public TMP_Text ageText;
    public TMP_Text descriptionText;

    Audition auditionData;
    int index = 0;

    public void SetView(int index, Audition audition)
    {
        auditionData = audition;
        this.index = index;
        if (auditionData != null)
        {
            titleText.text = auditionData.topic;
            ageText.text = auditionData.age_to.ToString();
            icon.gameObject.SetActive(false);
            GameManager.Instance.downLoadManager.DownloadImage(audition.image_url, (sprite) => {
                if (sprite != null)
                {
                    icon.sprite = sprite;
                    icon.gameObject.SetActive(true);
                }
            });
        }
    }

    public void OnClickAction()
    {
        Debug.Log("OnClickAction ");

        ProjectsDetailedView.Instance.auditionDetails.Load(auditionData, (index) => {

            switch (index)
            {
                case 0: //Edit
                    CreateAuditionView.Instance.EditView(auditionData, Refresh);
                    break;
                case 1: //Delete

                    DeleteAudition();
                    break;
                case 2: //Close
                    break;
            }
        });
    }

    void Refresh(bool isUpdated)
    {
        if (isUpdated)
        {
            ProjectsDetailedView.Instance.Reload();
        }

    }

    void UpdateList()
    {
        Refresh(true);
    }

    void DeleteAudition()
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("id", auditionData.id);
        parameters.Add("user_id", DataManager.Instance.userInfo.id);
        parameters.Add("project_id", auditionData.project_id);
        //parameters.Add("project_cast_id",auditionData.project_cast_id.ToString());
        parameters.Add("topic", auditionData.topic);
        string endDate = DatePicker.Instance.GetDateString(auditionData.end_date);
        if(!string.IsNullOrEmpty(endDate))
            parameters.Add("end_date", endDate);
        parameters.Add("rate_of_pay", auditionData.rate_of_pay);

        parameters.Add("status","yes");
        GameManager.Instance.apiHandler.DeleteAudition(parameters, (status, response) => {
            Debug.Log("UpdateCreatedAudition : " + response);
            if (status)
            {
                AlertModel alertModel = new AlertModel();
                alertModel.message = "Audition Deleted Successfully";
                alertModel.okayButtonAction = UpdateList;
                alertModel.canEnableTick = true;
                CanvasManager.Instance.alertView.ShowAlert(alertModel);
            }
            else
            {
                AlertModel alertModel = new AlertModel();
                alertModel.message = "Deleting Audition Failed";
                CanvasManager.Instance.alertView.ShowAlert(alertModel);
            }
        });
    }
}
