# XML Keyword-Driven automation tool
### Descript Test Case in XML without code,

### Support: GlobalData、Test Case's Data、Step with keyword action、Common Scenario Called、Input Parms、Output Params

### demo：

<?xml version="1.0" encoding="utf-8" ?>
<DataTable>
  <GlobalData>
    <LogPath>c:\log</LogPath>
    <Uri>http://www.baidu.com/</Uri>
    <SeachedText>lennie chen</SeachedText>
  </GlobalData>


  <TestScripts>
    <TestCase Id="1" Description="New and verify policy">
      <Scenario Enable="true" Id="1" Description="google search">
        <Action>1</Action>
        <Step ControlType="WebEdit" Method="GetText" Input="" ControlName="searchbox"><Output>SearchedText</Output></Step>
        <Action>2</Action>
        <Step ControlType="WebInputButton" Method="Click" Input="" ControlName="searchbtn"></Step>
      </Scenario>
      <Scenario Enable="false" Id="2" Description="">
        <Action>1</Action>
      </Scenario>
    </TestCase>
  </TestScripts>

  <UIMaps>
    <Type Name="WebEdit">
      <Control ControlName="searchbox">
        <P PropertyName="Id">kw</P>
      </Control>
    </Type>
    <Type Name="WebInputButton">
      <Control ControlName="searchbtn">
        <P PropertyName="Id">su</P>
      </Control>
    </Type>
  </UIMaps>

  <Actions>
    <CommonAction Id="1" Description="">
      <Step ControlType="WebEdit" Method="Set" Input="(SeachedText)" ControlName="searchbox"></Step>
      <Step ControlType="WebInputButton" Method="Click" Input="" ControlName="searchbtn"></Step>
    </CommonAction>
    <CommonAction Id="2" Description="">
      <Step ControlType="WebEdit" Method="Set" Input="{SearchedText}" ControlName="searchbox"></Step>
    </CommonAction>
  </Actions>
</DataTable>
