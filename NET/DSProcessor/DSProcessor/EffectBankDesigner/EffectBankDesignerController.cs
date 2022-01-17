using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSProcessor.Contract;
using DSProcessor.ReadyState;
using Designer.Manager;
using Designer.Manager.Helper;
using System.Windows.Forms;
using DSProcessor.EffectDesigner;

namespace DSProcessor.EffectBankDesigner
{
  class EffectBankDesignerController : ControllerBase
  {
    private EffectBankBuilder bankManager;
    private ChainPrototypeManager protManager;

    private static EffectBankDesignerController controller;

    private EffectBankDesignerController()
      : base(new EffectBankDesignerUI("DSProcessor: Effect Bank Designer"))
    {
      _gui.Controller = this;
    }

    public static EffectBankDesignerController Instance
    {
      get
      {
        if (controller == null)
        {
          controller = new EffectBankDesignerController();
        }
        return controller;
      }
    }

    private static void Dispose()
    {
      controller = null;
      System.GC.Collect();
    }

    public override void Close()
    {
      ReadyStateController.Instance.DisplayUI(true);
      EffectBankDesignerController.Dispose();    
    }

    internal bool UnsavedChanges
    {
      get
      {
        return bankManager.UnsavedChanges;
      }
    }

    internal override void UpdateDisplay()
    {
      if (bankManager != null && bankManager.CurrentEditDetails != null)
      {
        BankDetails details = bankManager.CurrentEditDetails;
        List<ChainControl> controlsList = new List<ChainControl>();

        foreach (ChainDetails cd in details.Chains)
        {
          ChainControl cc = new ChainControl();
          cc.Controller = this;
          cc.BankMember = cd;
          controlsList.Add(cc);
        }
        
        ((EffectBankDesignerUI)_gui).SetChainList(controlsList.ToArray());

        string status = null;
        if (details.Name == "" || details.Name == null)
        {
          status = "new";
        }
        else
        {
          status = details.Name;
        }

        if (details.Unsaved)
        {
          status += (" (unsaved)");
        }
        ((StatusStrip)_gui.Controls[_gui.Controls.IndexOfKey("statusStrip")]).Items["lblFilename"].Text = status;
      }
      else
      {
        ((StatusStrip)_gui.Controls[_gui.Controls.IndexOfKey("statusStrip")]).Items["lblFilename"].Text = "No Effect-Bank loaded";
        ((EffectBankDesignerUI)_gui).SetChainList(new ChainControl[0]);
      }
      _gui.Refresh();
      DisplayUI(true);
    }

    public override ViewBase Open()
    {
      protManager = new ChainPrototypeManager();
      bankManager = new EffectBankBuilder();
      UpdateDisplay();
      DisplayUI(true);
      return _gui;
    }

    internal void CreateNewBank()
    {
      bool goAhead = true;
      if (bankManager.UnsavedChanges)
      {
        DialogResult result = MessageBox.Show("Current chain has unsaved changes, continue?", "Confirm discard", MessageBoxButtons.OKCancel);
        goAhead = (result == DialogResult.OK);
      }
      if (goAhead)
      {
        bankManager.CreateNew();
        UpdateDisplay();
      }
    }

    internal void AddSavedChain()
    {
      if (bankManager.CurrentEditDetails == null)
      {
        _gui.DisplayError("Please open or create a new effect-bank first", "No bank loaded");
      }
      else
      {
        ChainSelectorUI select = new ChainSelectorUI(this, "Select a saved chain..");
        select.Enabled = true;
        select.Visible = true;
      }
    }

    internal ChainDetails[] CollectPrototypesList()
    {
      protManager = new ChainPrototypeManager();
      return protManager.DescribeChains().ToArray();
    }

    internal void SelectChainToAdd(ChainDetails chainDetails)
    {
      bankManager.AddChain(chainDetails, protManager);
      UpdateDisplay();
    }

    internal void SaveCurrentBank()
    {
      if (bankManager.CurrentEditDetails == null)
      {
        _gui.DisplayError("Please open or create a new effect-bank first", "No bank loaded");
      }
      else
      {
        if (bankManager.UnsavedChanges)
        {
          if (!bankManager.CurrentHasFile)
          {
            SaveCurrentBankAsNew();
          }
          else
          {
            bankManager.SaveCurrent();
            UpdateDisplay();
          }
        }
      }
    }

    internal void SaveCurrentBankAsNew()
    {
      if (bankManager.CurrentEditDetails == null)
      {
        _gui.DisplayError("Please open or create a new effect-bank first", "No bank loaded");
      }
      else
      {
        BankSaveUI saveDisplay = new BankSaveUI(this, "Save current effect bank..");
        BankPrototypeManager bpManager = new BankPrototypeManager();
        saveDisplay.UpdateBankList(bpManager.DescribeBanks().ToArray());
        saveDisplay.BringToFront();
        saveDisplay.Enabled = true;
        saveDisplay.Visible = true;
        saveDisplay.Focus();
      }
    }

    internal void SaveBankAsSpecified(string name, BankDetails selected, BankSaveUI saveUI)
    {
      try
      {
        if (selected != null && name == selected.Name)
        {
          DialogResult result = MessageBox.Show("Overwrite existing?", "Confirm", MessageBoxButtons.OKCancel);
          if (result == DialogResult.OK)
          {
            //overwrite existing
            bankManager.SaveCurrentAs(selected.Name);
          }
        }
        else
        {
          //save as filename texfield
          bankManager.SaveCurrentAs(name); 
        }
        saveUI.Close();
        UpdateDisplay();
      }
      catch (Exception e)
      {
        _gui.DisplayError(e.Message, "Error saving file..");
      }   
    }

    internal void ShiftChainUp(ChainDetails bankMember)
    {
      bankManager.IncrementIndex(bankMember);
      UpdateDisplay();
    }

    internal void ShiftChainDown(ChainDetails bankMember)
    {
      bankManager.DecrementIndex(bankMember);
      UpdateDisplay();
    }

    internal void RemoveFromBank(ChainDetails bankMember)
    {
      bankManager.RemoveFromBank(bankMember);
      UpdateDisplay();
    }

    internal void OpenSavedBank()
    {
      bool goAhead = true;
      if (bankManager.UnsavedChanges)
      {
        DialogResult result = MessageBox.Show("Current bank has unsaved changes, continue?", "Confirm discard", MessageBoxButtons.OKCancel);
        goAhead = (result == DialogResult.OK);
      }
      if (goAhead)
      {
        BankOpenUI openDisplay = new BankOpenUI(this, "Open a saved bank..");
        BankPrototypeManager bpManager = new BankPrototypeManager();
        List<BankDetails> banks = bpManager.DescribeBanks();
        openDisplay.UpdateBankList(banks.ToArray());
        openDisplay.BringToFront();
        openDisplay.Enabled = true;
        openDisplay.Visible = true;
        openDisplay.Focus();
      }
    }

    internal void OpenSelectedBank(BankDetails bankDetails)
    {
      try
      {
        bankManager.LoadBank(bankDetails); 
      }
      catch (Exception e)
      {
        _gui.DisplayError(e.Message, "A problem occurred in retrieving the file..");
      }
      UpdateDisplay();
    }

    internal void ViewChainDetails(ChainDetails bankMember)
    {
      ChainDetailsUI detailsDisplay = new ChainDetailsUI(this, "Details of chain: " + bankMember.Name);
      StringBuilder toDisplay = new StringBuilder(bankMember.Name + Environment.NewLine);
      for (int i = 0; i < bankMember.Effects.Count; i++)
      {
        toDisplay.AppendFormat("\t{0}: {1}{2}", i + 1, bankMember.Effects[i].Name, Environment.NewLine);
      }
      detailsDisplay.ShowString(toDisplay.ToString());
      detailsDisplay.Enabled = true;
      detailsDisplay.Visible = true;
      //detailsDisplay.Focus();
    }

  }
}
