[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/uM_GSLJS)
# Food-Drink
Final assignment
# 🍽️ Recipe Kitchen

A cross-platform mobile application developed with **.NET MAUI** for the **"Food and Drink"** theme.  
It allows users to manage recipes, scan barcodes, use voice assistance, and interact with device sensors.

---

## 📱 Features

### Core Functionality
- **Recipe Management** – Create, read, update, and delete recipes.
- **Search & Filter** – Search by name/ingredients and filter by category (Breakfast, Lunch, Dinner, Dessert).
- **Recipe Details** – View full information including nutrition facts (calories, protein, fat, carbs), ingredients, and step-by-step instructions.
- **Dark / Light Theme** – Toggle theme with proper contrast for accessibility.

### Hardware Integration (≥4 features)
| Hardware | Usage |
|----------|-------|
| **Camera** | Scan barcodes (EAN‑13, QR Code, etc.) using `Camera.MAUI` + ZXing |
| **Vibration** | Provides haptic feedback on successful barcode scan |
| **Microphone / Text‑to‑Speech** | Voice assistant reads user‑entered text aloud |
| **Accelerometer** | Displays real‑time device tilt (X, Y, Z) |

### Validation & Error Handling
- Input validation on add/edit pages (name, ingredients, instructions cannot be empty).
- Friendly error messages and `try-catch` blocks prevent crashes.

### Accessibility
- Semantic descriptions for all buttons and interactive elements.
- Screen reader announcements (`SemanticScreenReader.Announce`) for important actions.
- Supports WCAG contrast guidelines via theme switching.

---

## 🛠️ Technology Stack

- **Framework**: .NET MAUI (.NET 8)
- **Language**: C# / XAML
- **Architecture**: MVVM with `CommunityToolkit.Mvvm` (`ObservableProperty`, `RelayCommand`)
- **Dependency Injection**: Built‑in `IServiceCollection`
- **Barcode Scanning**: `Camera.MAUI` + `Camera.MAUI.ZXing`
- **Text‑to‑Speech**: `TextToSpeech.Default` (built‑in .NET MAUI API)
- **Sensors**: `Microsoft.Maui.Devices.Sensors.Accelerometer`
- **Vibration**: `Vibration.Default`

---

## 🚀 Getting Started

### Prerequisites
- Visual Studio 2022 (17.4 or later) with **.NET Multi‑platform App UI** workload installed.
- Android SDK / emulator (for Android deployment) or Windows development machine.


📂 Project Structure

Assignment/
├── Models/                 # Recipe entity
├── ViewModels/             # Recipe, BarcodeScan, VoiceAssistant, Sensors VMs
├── Views/                  # MainPage, RecipeDetailPage, AddEditRecipePage, etc.
├── Converters/             # ListToStringConverter for ingredients/steps
├── Resources/              # Images, fonts, styles (Colors.xaml, Styles.xaml)
├── Platforms/              # Android, iOS, Windows platform-specific code
├── App.xaml                # Application resources
├── AppShell.xaml           # Shell navigation and route registration
└── MauiProgram.cs          # Dependency injection & app initialization

🔧 Configuration Notes
Android Manifest
Platforms/Android/AndroidManifest.xml includes:

<uses-permission android:name="android.permission.CAMERA" />
<uses-permission android:name="android.permission.RECORD_AUDIO" />
<uses-permission android:name="android.permission.VIBRATE" />
<queries>
    <intent>
        <action android:name="android.intent.action.TTS_SERVICE" />
    </intent>
</queries>
Windows Manifest
Platforms/Windows/Package.appxmanifest includes:

<rescap:Capability Name="webcam" />
<rescap:Capability Name="microphone" />

🧪 Testing
The app has been manually tested on:

Android 13+ (physical device & emulator)

Windows 11 (Windows Machine)

All core scenarios (CRUD, search, filter, barcode scan, voice assistant, accelerometer, theme switching) function correctly on both platforms.
