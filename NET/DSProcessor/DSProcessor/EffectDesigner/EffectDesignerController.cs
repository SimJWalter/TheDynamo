using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSProcessor.Contract;
using Designer.Manager;
using DSProcessor.ReadyState;
using System.Windows.Forms;
using Designer.Manager.Helper;
using Designer.Entity;
using Processor;

namespace DSProcessor.EffectDesigner
{
  class EffectDesignerController : ControllerBase
  {
    private EffectPrototypeManager epManager;
    private EffectChainBuilder chManager;
    private ProcessHarness _processor;

    private static EffectDesignerController controller;

    private EffectDesignerController()
      : base(new EffectDesignerUI("DSProcessor: Effect Designer"))
    {
      _gui.Controller = this;
      _processor = new ProcessHarness();
    }

    internal static EffectDesignerController Instance
    {
      get
      {
        if (controller == null)
        {
          controller = new EffectDesignerController();
        }
        return controller;
      }
    }

    private static void Dispose()
    {
      controller = null;
      System.GC.Collect();
    }

    public override ViewBase Open()
    {
      epManager = new EffectPrototypeManager();
      chManager = new EffectChainBuilder();
      UpdateDisplay();
      DisplayUI(true);
      return _gui;
    }

    public override void Close()
    {
      ReadyStateController.Instance.DisplayUI(true);
      EffectDesignerController.Dispose(); 
    }

    internal bool UnsavedChanges
    {
      get 
      {
        return chManager.UnsavedChanges;
      }
    }

    internal void CreateNewChain()
    {
      bool goAhead = true;
      if (chManager.UnsavedChanges)
      {
        DialogResult result = MessageBox.Show("Current chain has unsaved changes, continue?", "Confirm discard", MessageBoxButtons.OKCancel);
        goAhead = (result == DialogResult.OK);
      }
      if (goAhead)
      {
        chManager.CreateNew();
        UpdateDisplay();
      }
    }

    internal override void UpdateDisplay()
    {
      List<Control> ctls = new List<Control>();

      if (chManager != null && chManager.CurrentEditDetails != null)
      {
        ChainDetails details = chManager.CurrentEditDetails;
        List<EffectControl> controlsList = new List<EffectControl>();

        for (int i = 0; i < details.Effects.Count; i++)
        {
          EffectControl ec = new EffectControl();
          ec.Controller = this;
          ec.ChainMember = details.Effects[i];
          controlsList.Add(ec);
        }
        ((EffectDesignerUI)_gui).SetEffectList(controlsList.ToArray());

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
        ((StatusStrip)_gui.Controls[_gui.Controls.IndexOfKey("statusStrip")]).Items["lblFilename"].Text = "No Effect-Chain loaded";
        ((EffectDesignerUI)_gui).SetEffectList(new EffectControl[0]);
      }
      _gui.Refresh();
      DisplayUI(true);
    }

    internal void AddEffectToChain()
    {
      if (chManager.CurrentEditDetails == null)
      {
        _gui.DisplayError("Please open or create a new effect-chain first", "No chain loaded");
      }
      else
      {
        EffectSelectorUI select = new EffectSelectorUI(this, "Select an effect..");
        select.Enabled = true;
        select.Visible = true;
      }
    }

    #region EffectSelectorUI
    internal EffectDetails[] CollectPrototypesList()
    {
      epManager = new EffectPrototypeManager();
      return epManager.DescribePrototypes().ToArray();
    }

    internal void SelectEffectToAdd(EffectDetails details)
    {
      chManager.AddEffect(details, epManager);
      UpdateDisplay();
    }
    #endregion

    #region ConfigEditorUI
    internal void OpenConfigForEdit(EffectDetails effect)
    {
      ConfigEditorUI editor = new ConfigEditorUI(this, "Edit configuration..");
      editor.ChainMember = effect;
      ConfigParameter[] cfgs = chManager.GetConfigParams(effect);
      List<ConfigControl> controls = new List<ConfigControl>();
      for (int i = 0; i < cfgs.Length; i++)
      {
        ConfigControl cControl = new ConfigControl();
        cControl.ParamName = cfgs[i].Name;
        cControl.ParamValue = cfgs[i].Value;
        cControl.Location = new System.Drawing.Point(0, 0 + (i * cControl.Height));
        controls.Add(cControl);
      }
      editor.DisplayConfigControls(controls.ToArray());
      editor.Enabled = true;
      editor.Visible = true;
    }

    internal void MakeConfigUpdate(ConfigControl[] cParams, EffectDetails cMember)
    {
      ConfigParameter[] cps = new ConfigParameter[cParams.Length];
      for (int i = 0; i < cps.Length; i++)
      {
        cps[i] = new ConfigParameter(cParams[i].ParamName, cParams[i].ParamValue);
      }

      chManager.UpdateChainMemberConfig(cMember, cps);
      DisplayUI(true);
      UpdateDisplay();
    }
    #endregion

    #region EffectControl
    internal void ShiftEffectUp(EffectDetails details)
    {
      chManager.IncrementIndex(details);
      UpdateDisplay();
    }

    internal void ShiftEffectDown(EffectDetails details)
    {
      chManager.DecrementIndex(details);
      UpdateDisplay();
    }

    internal void RemoveFromChain(EffectDetails details)
    {
      chManager.RemoveFromChain(details);
      UpdateDisplay();
    }
    #endregion

    internal void SaveCurrentChain()
    {
      if (chManager.CurrentEditDetails == null)
      {
        _gui.DisplayError("Please open or create a new effect-chain first", "No chain loaded");
      }
      else
      {
        if (chManager.UnsavedChanges)
        {
          if (!chManager.CurrentHasFile)
          {
            SaveCurrentChainAsNew();
          }
          else
          {
            chManager.SaveCurrent();
            UpdateDisplay();
          }
        }
      }
    }

    internal void SaveCurrentChainAsNew()
    {
      if (chManager.CurrentEditDetails == null)
      {
        _gui.DisplayError("Please open or create a new effect-chain first", "No chain loaded");
      }
      else
      {
        ChainSaveUI saveDisplay = new ChainSaveUI(this, "Save current effect chain..");
        saveDisplay.UpdateChainList(new ChainPrototypeManager().DescribeChains().ToArray());
        saveDisplay.BringToFront();
        saveDisplay.Enabled = true;
        saveDisplay.Visible = true;
        saveDisplay.Focus();
      }      
    }

    internal void SaveChainAsSpecified(string name, ChainDetails selected, ChainSaveUI saveUI)
    {
      try
      {
        if (selected != null && name == selected.Name)
        {
          DialogResult result = MessageBox.Show("Overwrite existing?", "Confirm", MessageBoxButtons.OKCancel);
          if(result == DialogResult.OK)
          {
            //overwrite existing
            chManager.SaveCurrentAs(selected.Name);
          }
        }
        else
        { 
          //save as filename texfield
          chManager.SaveCurrentAs(name);
        }
        saveUI.Close();
        UpdateDisplay();
      }
      catch (Exception e)
      {
        _gui.DisplayError(e.Message, "Error saving file..");
      }      
    }

    internal void OpenSavedChain()
    {
      bool goAhead = true;
      if (chManager.UnsavedChanges)
      {
        DialogResult result = MessageBox.Show("Current chain has unsaved changes, continue?", "Confirm discard", MessageBoxButtons.OKCancel);
        goAhead = (result == DialogResult.OK);
      }
      if (goAhead)
      {
        ChainOpenUI openDisplay = new ChainOpenUI(this, "Open saved chain..");
        ChainPrototypeManager cpManager = new ChainPrototypeManager();
        List<ChainDetails> chains = cpManager.DescribeChains();
        openDisplay.UpdateChainList(chains.ToArray());
        openDisplay.BringToFront();
        openDisplay.Enabled = true;
        openDisplay.Visible = true;
        openDisplay.Focus();
      }      
    }

    internal void OpenSelectedChain(ChainDetails chainDetails)
    {
      try
      {
        chManager.LoadChain(chainDetails);
      }
      catch (Exception e)
      {
        _gui.DisplayError(e.Message, "A problem occurred in retrieving the file..");
      }
      UpdateDisplay();
    }

    internal void TestRunCurrentChain()
    {
      if (_processor.IsRunning)
      {
        _processor.Stop();
        _processor.Destroy();
        _processor = new ProcessHarness();
      }
      else
      {
        EffectChain ec = chManager.CurrentEdit;
        if (ec.Configurations.Count > 0)
        {
          System.Windows.Forms.Control.ControlCollection list = ((EffectDesignerUI)this._gui).GetEffectList();
          if (ec.Configurations.Count == list.Count)
          {
            EffectChain newChain = new EffectChain();
            newChain.Name = "test_chain";

            for (int i = 0; i < ec.Configurations.Count; i++)
            {
              if (!((EffectControl)list[i]).Muted)
              {
                newChain.AppendEffect(ec.Configurations[i]);
              }
            }
            if (newChain.Configurations.Count > 0)
            {
              _processor.SelectDriver(_processor.GetDriverDetails()[0]);
              Designer.Entity.EffectBank eb = new Designer.Entity.EffectBank();
              eb.Name = "test_bank";
              eb.AppendChain(newChain);
              _processor.LoadBank(eb);
              _processor.Go();        
            }
          }
        }
      }
    }
  }
}
