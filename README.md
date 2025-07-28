# FIFA World Cup Stats Viewer (.NET)

This project provides a desktop-based solution for viewing and analyzing statistics from the 2018 Men's and 2019 Women's FIFA World Cups using data from a public API.

## 🧩 Components
- **DataLayer** – Class Library for data fetching, parsing and storage (shared by both UI apps)
- **WinFormsApp** – User-friendly Windows Forms interface with custom player controls and settings
- **WPFApp** – Responsive WPF interface for advanced match visualisation and animations

## 🌐 Features
- Asynchronous API or JSON file-based data loading
- Multi-language support (EN/HR), settings persisted on disk
- Favorite team and players selection with drag-and-drop
- Player and match rankings (goals, cards, attendance)
- PDF export of rankings and statistics
- Visualization of player positions on a football pitch (WPF)

## 🔧 Technologies
- C# (.NET Framework)
- Windows Forms
- Windows Presentation Foundation (WPF)
- JSON.NET (Newtonsoft)
- iTextSharp (for PDF)

## 📦 Getting Started
1. Clone the repository
2. Open the solution in Visual Studio
3. Set `WinFormsApp` or `WPFApp` as the startup project
4. Run the project and select a championship and language

> Note: Ensure internet access for live API data, or use provided JSON files as fallback.

## 📁 Data Source
Data retrieved from: [https://worldcup-vua.nullbit.hr](https://worldcup-vua.nullbit.hr)

## 📝 License
Project developed for academic purposes at Algebra University College (2025).
