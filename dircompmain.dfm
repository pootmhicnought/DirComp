object Form1: TForm1
  Left = 351
  Top = 102
  Caption = 'Dir Compare'
  ClientHeight = 576
  ClientWidth = 705
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  Position = poScreenCenter
  OnCreate = FormCreate
  OnDestroy = FormDestroy
  TextHeight = 13
  object Panel1: TPanel
    Left = 0
    Top = 0
    Width = 705
    Height = 105
    Align = alTop
    TabOrder = 0
    DesignSize = (
      705
      105)
    object Label3: TLabel
      Left = 16
      Top = 27
      Width = 31
      Height = 13
      Caption = 'Path 1'
    end
    object Label4: TLabel
      Left = 16
      Top = 54
      Width = 31
      Height = 13
      Caption = 'Path 2'
    end
    object sbPath1: TSpeedButton
      Left = 634
      Top = 23
      Width = 23
      Height = 22
      Anchors = [akTop, akRight]
      Caption = '...'
      OnClick = sbPath1Click
      ExplicitLeft = 1129
    end
    object sbPath2: TSpeedButton
      Left = 634
      Top = 51
      Width = 23
      Height = 22
      Anchors = [akTop, akRight]
      Caption = '...'
      OnClick = sbPath2Click
      ExplicitLeft = 1129
    end
    object edPath1: TEdit
      Left = 72
      Top = 24
      Width = 556
      Height = 21
      Anchors = [akLeft, akTop, akRight]
      TabOrder = 0
    end
    object edPath2: TEdit
      Left = 72
      Top = 51
      Width = 556
      Height = 21
      Anchors = [akLeft, akTop, akRight]
      TabOrder = 1
    end
    object cbDiffDetails: TCheckBox
      Left = 72
      Top = 78
      Width = 209
      Height = 17
      Caption = 'Output Diff Details'
      TabOrder = 2
    end
  end
  object Panel2: TPanel
    Left = 0
    Top = 105
    Width = 705
    Height = 471
    Align = alClient
    TabOrder = 1
    object Splitter1: TSplitter
      Left = 297
      Top = 1
      Height = 397
      ExplicitHeight = 359
    end
    object lboxSource: TListBox
      Left = 1
      Top = 1
      Width = 296
      Height = 397
      Align = alLeft
      ItemHeight = 13
      TabOrder = 0
    end
    object lboxDest: TListBox
      Left = 300
      Top = 1
      Width = 404
      Height = 397
      Align = alClient
      ItemHeight = 13
      TabOrder = 1
    end
    object Panel3: TPanel
      Left = 1
      Top = 398
      Width = 703
      Height = 72
      Align = alBottom
      BevelOuter = bvNone
      TabOrder = 3
      object Label1: TLabel
        Left = 128
        Top = 16
        Width = 82
        Height = 13
        Caption = 'Process Progress'
      end
      object Label2: TLabel
        Left = 128
        Top = 32
        Width = 60
        Height = 13
        Caption = 'File Progress'
      end
      object Button1: TButton
        Left = 8
        Top = 16
        Width = 75
        Height = 25
        Caption = 'GO'
        TabOrder = 0
        OnClick = Button1Click
      end
      object pbar: TProgressBar
        Left = 224
        Top = 16
        Width = 353
        Height = 16
        Smooth = True
        TabOrder = 1
      end
      object statusbar: TStatusBar
        Left = 0
        Top = 53
        Width = 703
        Height = 19
        Panels = <
          item
            Width = 500
          end
          item
            Width = 10
          end>
      end
      object pbarFile: TProgressBar
        Left = 224
        Top = 32
        Width = 353
        Height = 16
        Smooth = True
        TabOrder = 3
      end
      object btnCancel: TButton
        Left = 8
        Top = 16
        Width = 75
        Height = 25
        Caption = 'Cancel'
        TabOrder = 4
        Visible = False
        OnClick = btnCancelClick
      end
    end
    object memoReport: TMemo
      Left = 300
      Top = 1
      Width = 404
      Height = 397
      Align = alClient
      ScrollBars = ssBoth
      TabOrder = 2
      Visible = False
    end
  end
  object OpenDialog1: TOpenDialog
    Left = 352
    Top = 281
  end
end
