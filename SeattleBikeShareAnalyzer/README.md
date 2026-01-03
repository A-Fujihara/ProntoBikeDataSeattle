# Seattle Bike Share Analyzer

A C# console application that robustly loads, validates, and analyzes Seattle Pronto Bike Share trip data, demonstrating professional error handling and data validation techniques.

## Project Overview

This project processes 286,000+ bike trip records from the Seattle Pronto Cycle Share dataset (2014-2017), handling real-world data quality issues gracefully while maintaining data integrity.

## Features

- **Robust CSV Parsing**: Custom line-by-line CSV parser that handles corrupted data sections
- **Error Tracking**: Comprehensive error logging with categorized error types (parse errors, validation errors, skipped rows)
- **Data Validation**: Field-level validation with informative error messages
- **Production-Quality Error Handling**: Structured error reporting with LoadResult and LoadError classes
- **Performance**: Processes 286k+ records efficiently with progress reporting

## Architecture

### Classes

- **Trip.cs**: Data model representing a single bike trip
- **CsvErrorHandler.cs**: Core data loading logic with error handling
- **LoadResult.cs**: Container for successful trips and error information
- **LoadError.cs**: Individual error record with type, row number, and message
- **Program.cs**: Entry point and demonstration code

## Data Processing

### Input
- Source: [Seattle Pronto Cycle Share Dataset](https://www.kaggle.com/datasets/pronto/cycle-share-dataset)
- File: trip.csv (286,857 records)
- Format: CSV with 12 columns

### Output
- Successfully loaded: 286,857 trips
- Identified corrupted rows: 2 (rows 1 and 50,794)
- Error log: data_load_errors.log

### Handled Data Issues
- Duplicate header rows mid-file
- Malformed CSV rows
- Missing optional fields (gender, birth year)
- Type conversion errors

## How It Works

1. **Read**: File is read line-by-line instead of relying on CSV libraries
2. **Parse**: Each line is parsed using custom CSV parsing logic that handles quoted fields
3. **Validate**: Fields are validated with appropriate error messages
4. **Log**: Errors are categorized and logged to file and console
5. **Report**: Summary statistics show load success rate and error breakdown

## Building & Running
```bash
# Build
dotnet build

# Run
dotnet run
```

The application will:
- Load the trip data from the configured file path
- Report progress every 50,000 rows
- Display a summary of successful loads and errors
- Write detailed error log to `data_load_errors.log`

## Next Steps

- [ ] Add statistical analysis (mean, median, std dev of trip duration)
- [ ] Implement trend analysis (busiest hours, popular routes, seasonal patterns)
- [ ] Create WinForms UI for interactive data exploration
- [ ] Export analyzed results to CSV and text reports
- [ ] Add unit tests for data validation and parsing

## Technical Highlights

- **Error Handling**: Demonstrates professional error categorization and logging without throwing exceptions to the user
- **Data Resilience**: Gracefully handles corrupted data instead of failing completely
- **Performance**: Efficiently processes large datasets with progress reporting
- **Clean Architecture**: Separated concerns with dedicated classes for data model, loading logic, and error tracking

## Author

Angela (CS Graduate, 3.8 GPA)
