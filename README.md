# Wacom STU-430 .NET Integration (WinForms Example)

This guide explains step by step how to set up a **Wacom STU-430 signature pad** to capture signatures in a **.NET Windows Forms application**.

---

## 📑 Table of Contents

- [✅ Prerequisites](#-prerequisites)
- [⚠️ Why not use the low-level STU SDK?](#️-why-not-use-the-low-level-stu-sdk)
- [🔑 Required DLLs](#-required-dlls)
- [⚙️ Registering the DLLs](#️-registering-the-dlls)
- [📦 Adding References in Visual Studio](#-adding-references-in-visual-studio)
- [🖊️ Using the SDK in C#](#️-using-the-sdk-in-c)
- [✅ Summary of Steps](#-summary-of-steps)
- [🎉 Result](#-result)

---

## ✅ Prerequisites

Before starting, install the following software on Windows:

1. **Wacom STU Driver**  
   - [`Wacom-STU-Driver`](https://developer.wacom.com)  
   - Ensures your STU-430 is detected under *Device Manager → Human Interface Devices*.

2. **Wacom Signature SDK**  
   - Example: `Wacom-Signature-SDK-x64-4.8.2`  
   - Provides the COM libraries (`FlSigCOM.dll`, `FlSigCapt.dll`, etc.) required for signature capture.

3. **Wacom SigCaptX (optional)**  
   - For browser-based capture.  
   - Not required for our WinForms app.

---

## ⚠️ Why not use the low-level STU SDK?

Initially we tried the **STU SDK (`wgssSTU.dll`)**.  
- It provides direct access to the pad and raw pen data.  
- However, it requires **manual rendering of pen strokes** and is more complex.  
- Sample code was incompatible with the modern .NET Interop version (missing `PenStatus`, `DataEvent`, etc.).  

👉 Because of this, we switched to the **higher-level Signature SDK (Florentis COM libraries)** which handles signature capture and rendering for us.

---

## 🔑 Required DLLs

After installing the Signature SDK, you should see these under:

```
C:\Program Files (x86)\Common Files\WacomGSS\
```

- `FlSigCOM.dll`  
- `FlSigCapt.dll`  
- `FIWizCOM.dll`

### Explanation of DLLs

| DLL Name         | Purpose |
|------------------|---------|
| **FlSigCOM.dll** | Core communication layer for signature operations. Provides interop with .NET through COM. |
| **FlSigCapt.dll** | Manages signature capture dialogs and interactions with the pad. |
| **FIWizCOM.dll** | Provides the wizard components for guided capture scenarios. Optional, but often needed for full SDK functionality. |

---

## ⚙️ Registering the DLLs

These DLLs are **COM components**. They must be registered in Windows so that .NET can locate them.

Run **Command Prompt as Administrator**, then execute:

```bash
regsvr32 "C:\Program Files (x86)\Common Files\WacomGSS\FlSigCOM.dll"
regsvr32 "C:\Program Files (x86)\Common Files\WacomGSS\FIWizCOM.dll"
regsvr32 "C:\Program Files (x86)\Common Files\WacomGSS\FlSigCapt.dll"
```

### Why do we need this step?

- `regsvr32` adds entries to the Windows Registry.  
- This allows **Visual Studio** and **.NET runtime** to recognize the COM components.  
- Without this step, references cannot be added in Visual Studio, and you will see errors like *"The type or namespace name does not exist"*.  

If successful, you’ll see:

```
DllRegisterServer in FlSigCOM.dll succeeded.
```

---

## 📦 Adding References in Visual Studio

1. In **Visual Studio**, right-click your project → **Add Reference…**  
2. Go to **COM → Type Libraries**.  
3. Select:  
   - *Florentis Signature Capture COM*  
   - *Florentis SigCtl COM*  
   - *Florentis Wizard COM* (optional)  
4. After adding, check the reference properties:  
   - `Copy Local = True`  
   - `Embed Interop Types = False`

Visual Studio will generate interop assemblies like:

- `Interop.FlSigCaptLib.dll`  
- `Interop.FLSIGCTLLib.dll`  
- `Interop.FIWizCOMLib.dll`

---
![Project Output](images/sigPadOutput.png)

## 🖊️ Using the SDK in C#

Now you can use the COM classes in your WinForms app:

```csharp
using FlSigCaptLib;
using FLSIGCTLLib;

private void Form1_Load(object sender, EventArgs e)
{
    SigCtl sigCtl = new SigCtl();
    DynamicCapture dc = new DynamicCaptureClass();

    DynamicCaptureResult res = dc.Capture(sigCtl, "Test User", "Title", null, null);

    if (res == DynamicCaptureResult.DynCaptOK)
    {
        MessageBox.Show("Signature captured successfully!");
    }
    else
    {
        MessageBox.Show("Capture failed: " + res.ToString());
    }
}
```

---

## ✅ Summary of Steps

1. Installed **STU driver** to make device detectable in Windows.  
2. Tried the **low-level STU SDK (`wgssSTU.dll`)**, but it required manual pen rendering and didn’t work with modern Interop.  
3. Installed the **higher-level Signature SDK (Florentis COM)**, which provides ready-made capture functionality.  
4. Registered COM DLLs (`FlSigCOM.dll`, `FlSigCapt.dll`, `FIWizCOM.dll`) with `regsvr32`.  
5. Added **COM References** in Visual Studio → generated `Interop.*` assemblies.  
6. Wrote a simple WinForms app using `SigCtl` + `DynamicCapture` to display and capture a signature.  

---

## 🎉 Result

- The STU-430 pad is fully working with a .NET WinForms application.  
- You can capture, view, and process signatures directly.  
- The setup is reusable for other Wacom devices supported by the SDK.
