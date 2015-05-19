namespace FluentAlerts

module Serializer = 

    let Concat ss = 
        ss|> String.concat ""  

    let ConcatAlerts serializeAlert alerts = 
        alerts 
        |> List.map serializeAlert 
        |> Concat

    let ConcatAlertsIndexed serializeAlert alerts = 
        alerts 
        |> List.mapi serializeAlert 
        |> Concat

    let ConcatCells serializeCell cells numberOfTableColumns =
        let cellMapi = cells |> List.length |> (serializeCell numberOfTableColumns)
        cells
        |> List.mapi cellMapi
        |> Concat

    let ConcatRows serializeRow rows = 
        let rowMapi = rows |> List.length |> serializeRow
        rows
        |> List.mapi rowMapi
        |> Concat 