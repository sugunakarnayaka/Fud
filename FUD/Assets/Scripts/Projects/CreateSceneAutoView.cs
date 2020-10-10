﻿using System.Collections.Generic;
using UnityEngine;


public class CreateSceneAutoView : MonoBehaviour
{
    public ScenesDocumentHandler filesHandler;


    CreateDialoguesView dialoguesView;

    List<Dictionary<string, object>> uploadedDict = new List<Dictionary<string, object>>();

    string mediaSource = "scenes";


    public void EnableView(CreateDialoguesView dialoguesView)
    {
        this.dialoguesView = dialoguesView;

        gameObject.SetActive(true);
    }

    public void OnMediaButtonAction()
    {
        GalleryManager.Instance.GetDocuments(mediaSource, OnDocumentsUploaded);
    }

    public void OnSaveButtonAction()
    {
        dialoguesView.SetAutoDialogues(uploadedDict);

        OnBackButtonAction();
    }

    public void OnBackButtonAction()
    {
        filesHandler.ClearData();

        //gameObject.SetActive(false);
    }

    void OnDocumentsUploaded(bool status, List<string> documentURLs)
    {
        if (status)
        {
            filesHandler.Load(GalleryManager.Instance.GetLoadedFiles(), false, EMediaType.Document);

            for (int i = 0; i < documentURLs.Count; i++)
            {
                Dictionary<string, object> kvp = new Dictionary<string, object>();

                kvp.Add("content_id", 1);

                kvp.Add("content_url", documentURLs[i]);

                kvp.Add("media_type", "document");

                uploadedDict.Add(kvp);
            }
        }
    }
}