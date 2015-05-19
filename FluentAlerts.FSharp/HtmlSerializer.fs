namespace FluentAlerts

module HtmlSerializer = 
    open Serializer 

    let styles = @"<style type='text/css'> table.alert-table { font-family:Arial,Sans-Serif; font-size:8pt; background-color: #FFFFFF; } th.value-emphasized{ background-color: #E0E0E0; font-weight:bold; font-size:10pt; } td.value-title{ background-color: #A0A0A0; font-size:10pt; font-weight:bold; } td.value-emphasized{ background-color: #D0D0D0; font-weight:bold; } td.value-normal { background-color: #F0F0F0; } td#first-column.value-normal{ background-color: #E0E0E0; } </style>"
    
    let private GetCellId cellColumn rowColumns = 
        if cellColumn = rowColumns then " id='last-column'"
        elif cellColumn = 1 then " id='first-column'"
        else ""

    let private GetCellColspan cellColumn rowColumns numberOfTableColumns = 
        if cellColumn = rowColumns && (numberOfTableColumns - cellColumn) > 0 
        then (sprintf " colspan='%i'" (1 + numberOfTableColumns - cellColumn))
        else ""    

    let rec ConcatAlertAsHtml convertObjectToAlert alert =
        let concatAlerts = ConcatAlerts (ConcatAlertAsHtml convertObjectToAlert)
        match alert with
        | Document (alerts) -> sprintf "<div class='alert-document'>%s</div>" (concatAlerts alerts)
        | OrderedList (alerts) -> sprintf "<ol>%s</ol>" (concatAlerts alerts)
        | UnOrderedList (alerts) -> sprintf "<ul>%s</ul>" (concatAlerts alerts)
        | TextBlock (alerts) -> sprintf "<p>%s</p>" (concatAlerts alerts)
        | HeaderTextBlock (level, alerts) -> sprintf "<H%i>%s</H%i>" level (concatAlerts alerts) level
        | HorizontalRule -> "<HR>"
        | Link (url, text) -> sprintf "<a href='%s'>%s</a>" url text 
        | CodeBlock (_, code) -> sprintf "<pre><code>%s</code></pre>" code
        | Text (s) -> s
        | Italic (s) -> sprintf "<i>%s</i>" s
        | Underscore (s) -> sprintf "<ins>%s</ins>" s
        | Bold (s) -> sprintf "<b>%s</b>" s
        | Strikethrough (s) -> sprintf "<del>%s</del>" s
        | NewLine -> "<BR>"
        | Table (rows) -> sprintf "<table class='alert-table' cellspacing='1' cellpadding='2' width='100%%'>%s</table>" (ConcatRows (rowSerializer convertObjectToAlert) rows)
        | Content (o) -> o |> convertObjectToAlert |> (ConcatAlertAsHtml convertObjectToAlert) 
        | _ -> ""
    and rowSerializer = (fun convertObjectToAlert numberOfTableColumns i row -> sprintf "<TR>%s</TR>" (ConcatCells (cellSerializer convertObjectToAlert) row.Cells numberOfTableColumns))
    and cellSerializer = (fun convertObjectToAlert numberOfTableColumns numberOfRowColumns i cell -> 
        sprintf "<TD class='value-normal'%s%s>%s</TD>" (GetCellId i numberOfRowColumns) (GetCellColspan i numberOfRowColumns numberOfTableColumns) (ConcatAlertAsHtml convertObjectToAlert cell.Content))

    let ToHtml convertObjectToAlert alert =
        styles + (ConcatAlertAsHtml convertObjectToAlert alert)
