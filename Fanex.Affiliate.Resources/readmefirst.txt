/*web.config*/
<configuration>
    <appSettings>
        <add key="TextExpressionBuilder.Database" value="affiliate" />
        <add key="TextExpressionBuilder.SP" value="AffResourcesSel" />
    </appSettings>

    <system.web>
        <compilation>
              <expressionBuilders>
                    <add expressionPrefix="Text" type="Fanex.Affiliate.Resources.TextExpressionBuilder" />
              </expressionBuilders>
        </compilation>
    </system.web>
</configuration>


/*TextExpressionBuilder in Action*/
<asp:Literal ID="Literal1" Text="<%$ Text:YourKeyWordHere %>" runat="Server" />