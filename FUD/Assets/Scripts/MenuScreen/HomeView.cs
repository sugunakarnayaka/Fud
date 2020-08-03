﻿using System.IO;
using UnityEngine;

public class HomeView : BaseView
{
    public LeftMenu leftMenuPanel;

    public HomeBannersView bannersView;

    public HomeAlertsView alertsView;


    #region Ovveride Methods
    protected override void EnableView()
    {
        base.EnableView();
    }

    protected override void OnAddSubView(GameObject addedObject)
    {
        base.OnAddSubView(addedObject);
    }

    public override void OnRemoveLastSubView()
    {
        base.OnRemoveLastSubView();
    }

    public override void OnExitScreen()
    {
        base.OnExitScreen();
    }

    #endregion

    void Start()
    {
        string userFilePath = APIConstants.PERSISTENT_PATH + "UserInfo";

        string fileName = Path.GetDirectoryName(userFilePath);

        if (!Directory.Exists(userFilePath))
        {
            Directory.CreateDirectory(userFilePath);
        }

        userFilePath += "/Userinfo";

        string jsonData = File.ReadAllText(userFilePath);

        UserDataObject loginResponse = JsonUtility.FromJson<UserDataObject>(jsonData);

        DataManager.Instance.UpdateUserInfo(loginResponse.data);

        GameManager.Instance.apiHandler.GetAvailableActvities(null);

        bannersView.Load();

        alertsView.Load();
    }

    public void OnMenuButtonAction()
    {
        leftMenuPanel.SetView(OnCloseLeftMenu);
    }

    public void OnCloseLeftMenu()
    {

    }
}
