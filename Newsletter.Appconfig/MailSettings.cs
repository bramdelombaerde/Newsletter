﻿namespace Newsletter.Appconfig
{
    public class MailSettings
    {
        public string? From { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
    }
}