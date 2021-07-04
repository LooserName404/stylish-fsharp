namespace Chapter6

module Exercise06_01 =
    
    open System

    type MeterValue =
        | Standard of int
        | Economy7 of Day: int * Night: int
        
    type MeterReading =
        { ReadingDate: DateTime
          MeterValue: MeterValue }
    
    let formatReading { ReadingDate = readingDate; MeterValue = meterValue } =
        let dateString = readingDate.ToShortDateString()
        match meterValue with
        | Standard s -> sprintf "Your reading on: %s was %07i" dateString s
        | Economy7 (day, night) -> sprintf "Your readings on: %s Day: %07i Night: %07i" dateString day night
