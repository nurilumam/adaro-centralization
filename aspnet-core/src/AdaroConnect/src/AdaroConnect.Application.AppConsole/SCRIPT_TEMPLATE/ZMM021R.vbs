If Not IsObject(application) Then
   Set SapGuiAuto  = GetObject("SAPGUI")
   Set application = SapGuiAuto.GetScriptingEngine
End If
If Not IsObject(connection) Then
   Set connection = application.Children(0)
End If
If Not IsObject(session) Then
   Set session    = connection.Children(0)
End If
If IsObject(WScript) Then
   WScript.ConnectObject session,     "on"
   WScript.ConnectObject application, "on"
End If
session.findById("wnd[0]").maximize
session.findById("wnd[0]/tbar[0]/okcd").text = "ZMM021R"
session.findById("wnd[0]").sendVKey 0
session.findById("wnd[0]/usr/ctxtS_WERKS-LOW").setFocus
session.findById("wnd[0]/usr/ctxtS_WERKS-LOW").caretPosition = 0
session.findById("wnd[0]").sendVKey 4
session.findById("wnd[1]/usr/lbl[1,3]").caretPosition = 1
session.findById("wnd[1]").sendVKey 2
session.findById("wnd[0]/usr/ctxtS_WERKS-HIGH").setFocus
session.findById("wnd[0]/usr/ctxtS_WERKS-HIGH").caretPosition = 0
session.findById("wnd[0]").sendVKey 4
session.findById("wnd[1]/usr/lbl[1,11]").setFocus
session.findById("wnd[1]/usr/lbl[1,11]").caretPosition = 3
session.findById("wnd[1]").sendVKey 2
session.findById("wnd[0]/usr/ctxtS_BEDAT-LOW").text = "01.03.2023"
session.findById("wnd[0]/usr/ctxtS_BEDAT-HIGH").text = "29.03.2024"
session.findById("wnd[0]/usr/ctxtS_BEDAT-HIGH").setFocus
session.findById("wnd[0]/usr/ctxtS_BEDAT-HIGH").caretPosition = 10
session.findById("wnd[0]/tbar[1]/btn[8]").press
session.findById("wnd[0]/mbar/menu[0]/menu[3]/menu[1]").select
session.findById("wnd[1]/usr/ctxtDY_PATH").text = "D:\EXPORT"
session.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "ZMM021R_1YEAR.XLSX"
session.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 28
session.findById("wnd[1]/tbar[0]/btn[0]").press
session.findById("wnd[0]/tbar[0]/btn[15]").press
session.findById("wnd[0]/tbar[0]/btn[15]").press
