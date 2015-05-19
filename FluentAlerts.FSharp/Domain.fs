namespace FluentAlerts

type Cell = {ColumnNumber:int; MaxColumnNumber: int; Content: Alert}
 and Row = {RowNumber:int; MaxColumnNumber:int; Cells:Cell list}
 and Alert = 
    | Content of obj
    | Document of Alert list
    | OrderedList of Alert list
    | UnOrderedList of Alert list
    | TextBlock of Alert list
    | HeaderTextBlock of Level: int * Alert list
    | CodeBlock of Language: string * Code: string
    | Link of Url: string * Text: string
    | HorizontalRule
    | Text of string
    | Italic of string
    | Underscore of string
    | Bold of string
    | Strikethrough of string
    | NewLine    
    | Table of Rows: Row list
     

    
