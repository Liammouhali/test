﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BuckshotPlusPlus.WebServer
{
    internal class Page
    {
        public static string RenderWebPage(Token MyPage)
        {
            string HTML_code = "<!DOCTYPE html>" + Environment.NewLine +
                "<html lang=\"en\">" + Environment.NewLine +

                "<head>" + Environment.NewLine +
                 "<meta name=\"viewport\" content=\"width = device - width, initial - scale = 1.0\">" + Environment.NewLine +
                "<title>";

            TokenDataContainer MyPageContainer = (TokenDataContainer)MyPage.Data;
            TokenDataVariable MyPageTitle = TokenUtils.FindTokenDataVariableByName(MyPageContainer.ContainerData, "title");
            TokenDataVariable MyPageBody = TokenUtils.FindTokenDataVariableByName(MyPageContainer.ContainerData, "body");

            

            if (MyPageTitle != null)
            {
                HTML_code += MyPageTitle.VariableData;
            }
            else
            {
                HTML_code += MyPageContainer.ContainerName;
            }
            HTML_code += "</title>" + Environment.NewLine;

            Token MyPageFonts = TokenUtils.FindTokenByName(MyPageContainer.ContainerData, "fonts");

            if (MyPageFonts != null)
            {
                HTML_code += "<style>" + Environment.NewLine;
                foreach (Token ArrayValue in Analyzer.Array.GetArrayValues(MyPageFonts))
                {
                    TokenDataVariable ArrayVar = (TokenDataVariable)ArrayValue.Data;
                    HTML_code += "@import url('" + ArrayVar.VariableData + "');" + Environment.NewLine;
                }
                HTML_code += "</style>";
            }

            HTML_code += "</head>" + Environment.NewLine;

            if (MyPageBody != null)
            {
                Formater.DebugMessage(MyPageBody.VariableData);
                Formater.DebugMessage(TokenUtils.FindTokenByName(MyPage.MyTokenizer.FileTokens, MyPageBody.VariableData).ToString());
                HTML_code += Compiler.HTML.View.CompileView(TokenUtils.FindTokenByName(MyPage.MyTokenizer.FileTokens, MyPageBody.VariableData));
            }
            else
            {
                HTML_code += "<body><h1>" + MyPageContainer.ContainerName + "</h1></body>";
            }
          

            HTML_code += "</html>";

            return HTML_code;
        }
    }
}
