using UnityEngine;
using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

public static class ApiController
{
    public static string GetJwtKey()
    {
        try
        {
            string url = "http://20.15.114.131:8080/api/login";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "*/*";
            string body = "{\"apiKey\":\"NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmNlOjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJjNA\"}";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string jsonResponse = reader.ReadToEnd();
                JObject jsonObject = JObject.Parse(jsonResponse);
                string token = (string)jsonObject["token"];
                return token;
            }
        }
        catch (WebException ex)
        {
            Debug.LogError($"Error occurred during JWT key retrieval: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Debug.LogError($"An unexpected error occurred: {ex.Message}");
            return null;
        }
    }

    public static UserProfile GetUserProfile(string jwtKey)
    {
        if (string.IsNullOrEmpty(jwtKey))
        {
            Debug.LogError("JWT key is null or empty");
            return null;
        }

        try
        {
            string url = "http://20.15.114.131:8080/api/user/profile/view";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add("Authorization", "Bearer " + jwtKey);

            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            JObject jsonObject = JObject.Parse(jsonResponse);

            UserProfile userProfile = new UserProfile()
            {
                FirstName = (string)jsonObject["user"]["firstname"],
                LastName = (string)jsonObject["user"]["lastname"],
                UserName = (string)jsonObject["user"]["username"],
                Nic = (string)jsonObject["user"]["nic"],
                PhoneNumber = (string)jsonObject["user"]["phoneNumber"],
                Email = (string)jsonObject["user"]["email"],
                ProfilePictureUrl = (string)jsonObject["user"]["profilePictureUrl"]
            };

            PlayerPrefs.SetString("userName", userProfile.UserName);

            return userProfile;
        }
        catch (WebException ex)
        {
            Debug.LogError($"Error occurred during user profile retrieval: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Debug.LogError($"An unexpected error occurred: {ex.Message}");
            return null;
        }
    }

    public static string UpdateUserProfile(string jwtKey, UpdateProfileDTO updateProfileObject)
    {
        //Debug.Log(updateProfileObject.GetValueOrDefault("firstname"));
        string url = "http://20.15.114.131:8080/api/user/profile/update";

        string jsonData = JsonUtility.ToJson(updateProfileObject);
        Debug.Log(jsonData);
        byte[] myData = System.Text.Encoding.UTF8.GetBytes(jsonData);

        var putRequest = UnityWebRequest.Put(url, myData);

        putRequest.SetRequestHeader("Content-Type", "application/json");
        putRequest.SetRequestHeader("Authorization", "Bearer " + jwtKey);
        putRequest.SetRequestHeader("Accept", "application/json");

        putRequest.SendWebRequest();

        string response = putRequest.downloadHandler.text;
        Debug.Log(response);
        JObject jsonResponse = JObject.Parse(response);

        // Handle the response
        if (putRequest.result == UnityWebRequest.Result.ConnectionError || putRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error sending PUT request: " + putRequest.error);
            return "sendError";
        }
        else if (putRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Update successful");
            return "success";
        }
        else
        {
            if (jsonResponse.ContainsKey("message"))
            {
                Debug.LogError("Error updating user profile: " + (string)jsonResponse["message"]);
                return (string)jsonResponse["message"];
            }
            else
            {
                Debug.LogError("Unknown error occurred");
                return "unknownError";
            }
        }
    }
}
