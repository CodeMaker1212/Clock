using System;
using System.Threading;
using UnityEngine;

namespace Clock
{
    public class CurrentTimeFetcher
    {
        private readonly string _yandexTimeUrl = "https://yandex.ru";
        private readonly string _googleTimeUrl = "https://www.google.ru";

        public void GetTime(Action<Time> onSuccess)
        {
            TryGetTimeFromWeb(_yandexTimeUrl, (Time? yandexTime) =>
            {
                if (yandexTime != null)
                    onSuccess?.Invoke(yandexTime.Value);               
                else
                {
                    TryGetTimeFromWeb(_googleTimeUrl, (Time? googleTime) =>
                    {
                        if (googleTime != null)                      
                            onSuccess.Invoke(googleTime.Value);                      
                        else                      
                            onSuccess?.Invoke(GetSystemTime());                       
                    });
                }                               
            });           
        }
        private void TryGetTimeFromWeb(string url, Action<Time?> callback)
        {
            var www = new WWW(_googleTimeUrl);

            while (!www.isDone && www.error == null)
                Thread.Sleep(1);

            var str = www.responseHeaders["Date"];

            if (DateTime.TryParse(str, out DateTime dateTime))
            {
                int hours = dateTime.ToLocalTime().Hour;
                int minutes = dateTime.ToLocalTime().Minute;
                int seconds = dateTime.ToLocalTime().Second;
                callback?.Invoke(new Time(hours, minutes, seconds));             
            }
            else callback?.Invoke(null);                                  
        }

        private Time GetSystemTime()
        {
            return new Time
            (
                int.Parse(DateTime.Now.ToString("HH")),
                int.Parse(DateTime.Now.ToString("mm")),
                int.Parse(DateTime.Now.ToString("ss"))
            );
        }
    }
}