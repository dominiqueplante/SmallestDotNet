﻿<%@ WebHandler Language="C#" Class="SmallestDotNet" %>

using System;
using System.Web;
using SmallestDotNetLib;

public class SmallestDotNet : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        WriteToResponse("<span class=\"smallerdotnet\">");
        context.Response.ContentType = "text/javascript";

        WriteToResponse(Helpers.GetUpdateInformation(context.Request.UserAgent, context.Request.Browser.ClrVersion).Replace("'", @"\'").Replace(Environment.NewLine, "<br />"));
        WriteToResponse("</span>");
    }

    private void WriteToResponse(string s)
    {
        HttpResponse r = HttpContext.Current.Response;
        r.Write(String.Format("document.write('{0}')\r\n", s));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}