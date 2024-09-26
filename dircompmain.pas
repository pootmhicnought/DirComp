unit dircompmain;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, FileCtrl, ExtCtrls, abutils, ComCtrls, Vcl.Buttons;

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Panel2: TPanel;
    lboxSource: TListBox;
    lboxDest: TListBox;
    memoReport: TMemo;
    Panel3: TPanel;
    Button1: TButton;
    pbar: TProgressBar;
    statusbar: TStatusBar;
    Label1: TLabel;
    Label2: TLabel;
    pbarFile: TProgressBar;
    Splitter1: TSplitter;
    btnCancel: TButton;
    OpenDialog1: TOpenDialog;
    edPath1: TEdit;
    Label3: TLabel;
    Label4: TLabel;
    edPath2: TEdit;
    sbPath1: TSpeedButton;
    sbPath2: TSpeedButton;
    cbDiffDetails: TCheckBox;
    procedure Button1Click(Sender: TObject);
    procedure btnCancelClick(Sender: TObject);
    procedure sbPath1Click(Sender: TObject);
    procedure sbPath2Click(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private
    { Private declarations }
    Cancelled : Boolean;
    function PickDir(CurrDir: String): String;
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.DFM}

uses IniFiles;

procedure TForm1.Button1Click(Sender: TObject);
const
  SAME = 0;
  DEST_MISSING = 1;
  SOURCE_MISSING = 2;
  DIFF = 3;
  MISSING = '<n/a>';

  function CompareFiles(fn1, fn2: string;Index: Integer) : Integer;
  var
    fs1, fs2 : TFileStream;
    Buff1, Buff2 : array[1..4096] of byte;
    NumToRead, Loop : Integer;
    AbsPos: Integer;
    slFileDetails: TStringList;

  begin
    Result := DIFF;
    slFileDetails := TStringList.Create;
    fs1 := TFileStream.Create(edPath1.Text + fn1,fmOpenRead + fmShareDenyNone);
    try
      fs2 := TFileStream.Create(edPath2.Text + fn2,fmOpenRead + fmShareDenyNone);
      try
        if (fs1.Size = fs2.Size)
        then begin
          AbsPos := 0;
          slFileDetails.Clear;
          Result := SAME;
          pbarFile.Position := 0;
          pbarFile.Max := fs1.Size;
          while (fs1.Position < fs1.Size) and
               ((Result = SAME) or (cbDiffDetails.Checked))
          do begin
            Application.ProcessMessages;
            if (fs1.Size - fs1.Position > SizeOf(Buff1))
            then NumToRead := SizeOf(Buff1)
            else NumToRead := fs1.size - fs1.position;
            fs1.Read(Buff1,NumToRead);
            fs2.Read(Buff2,NumToRead);
            for Loop := 1 to NumToRead
            do begin    // Iterate
              Inc(AbsPos);
              if (Buff1[Loop] <> Buff2[Loop])
              then begin
                Result := DIFF;
                If (cbDiffDetails.Checked) then begin
                  slFileDetails.Add(IntToHex(AbsPos, 10) + ': ' + IntToHex(Buff1[Loop], 2) + ' ' + IntToHex(Buff2[Loop], 2));
                end;
              end;
            end;    // for
            pbarFile.Position := fs1.Position;
          end;    // while
        end;
      finally
        fs2.Free;
      end;
    finally
      fs1.Free;
      If (cbDiffDetails.Checked) and (slFileDetails.Count > 0) then begin
        lboxSource.Items.Objects[Index] := slFileDetails;
//        slFileDetails.SaveToFile(ExtractFilePath(ParamStr(0)) + ChangeFileExt(fn1, '.txt'))
        slFileDetails := TStringList.Create;
      end
      else FreeAndNil(slFileDetails);
      pbarFile.Position := 0;
    end;
  end;


  function DiffDetails(DeetsList: TStringList): String;
  var CurrFileIndex, LastFileIndex,
      BlockStart, BlockEnd, BlockCount,
      TotalCount: Integer;
      Index: Integer;
      ResultList: TStringList;
  begin
    Index := 0;
    BlockStart := 0;
    BlockEnd   := 0;
    BlockCount := 0;
    TotalCount := 0;
    LastFileIndex := 0;
    ResultList := TStringList.Create;
    while Index < DeetsList.Count do begin
      CurrFileIndex := StrToInt('$' + Copy(DeetsList[Index], 1, 10));
      if (CurrFileIndex > (LastFileIndex + 1)) then begin
        if BlockCount > 1 then
          ResultList.Add(Format('BlockStart: %d, BlockCount: %d', [BlockStart, BlockCount]));
        BlockStart := CurrFileIndex;
        BlockCount := 1;
      end
      else begin
        Inc(BlockCount);
      end;
      LastFileIndex := CurrFileIndex;
      Inc(TotalCount);
      Inc(Index);
    end;
    Result := ResultList.Text;
  end;

var
  Loop, Index : Integer;
  s: string;
  Failed: Integer;
  MissingCount: Integer;

begin
  memoReport.Visible := FALSE;
  memoReport.Lines.Clear;
  lboxSource.Items.Clear;
  lboxDest.Items.clear;
  screen.Cursor := crHourglass;
  button1.enabled := false;
  Cancelled := false;
  Failed := 0;
  btnCancel.Visible := TRUE;
  try
    statusbar.panels[0].text := 'Getting First Path Files';
    application.processmessages;
    AbFindFiles(edPath1.Text + '\*.*',faAnyFile and not faDirectory,lboxSource.Items,true);
    application.processmessages;
    statusbar.panels[0].text := 'Getting Second Path Files';
    application.processmessages;
    ABFindFiles(edPath2.Text + '\*.*',faAnyFile and not faDirectory,lboxDest.Items,true);
    statusbar.panels[0].text := 'Making paths relative';
    if (lboxSource.Items.Count > lboxDest.Items.Count)
    then begin
      pbar.Max := lboxSource.Items.Count;
      for Loop := 0 to lboxSource.Items.Count - 1
      do begin    // Iterate
        Application.ProcessMessages;
        s := lboxSource.Items[Loop];
        delete(s,1,length(edPath1.Text));
        lboxSource.Items[Loop] := s;
        if (Loop < lboxDest.Items.Count)
        then begin
          s := lboxDest.Items[Loop];
          delete(s,1,length(edPath2.Text));
          lboxDest.Items[Loop] := s;
        end;
        pbar.Position := Loop;
      end;    // for
    end
    else begin
      pbar.Max := lboxDest.Items.Count;
      for Loop := 0 to lboxDest.Items.Count - 1
      do begin    // Iterate
        Application.ProcessMessages;
        s := lboxDest.Items[Loop];
        delete(s,1,length(edPath2.Text));
        lboxDest.Items[Loop] := s;
        if (Loop < lboxSource.Items.Count)
        then begin
          s := lboxSource.Items[Loop];
          delete(s,1,length(edPath1.Text));
          lboxSource.Items[Loop] := s;
        end;
        pbar.Position := Loop;
      end;    // for
    end;
    lboxSource.Sorted := TRUE;
    lboxDest.Sorted := TRUE;
    lboxSource.Sorted := FALSE;
    lboxDest.Sorted := FALSE;
    pbar.Position := 0;
    statusbar.panels[0].text := 'Checking for missing files';
    Loop := 0;
    lboxSource.Items.SaveToFile('source.txt');
    lboxDest.Items.SaveToFile('dest.txt');
    while Loop < lboxSource.Items.Count
    do begin
      Application.ProcessMessages;
      if (Loop < lboxDest.Items.Count) and (lboxSource.Items[Loop] = lboxDest.Items[Loop])
      then Inc(Loop)
      else begin
        lboxSource.Items.SaveToFile('source.txt');
        lboxDest.Items.SaveToFile('dest.txt');
        Index := lboxDest.Items.IndexOf(lboxSource.Items[Loop]);
        if (Index = -1)
        then lboxDest.Items.Insert(Loop,Missing)
        else begin
          MissingCount := Index - Loop;
          while (MissingCount > 0)
          do begin
            lboxSource.Items.Insert(Loop,Missing);
            Dec(MissingCount);
            lboxSource.Items.SaveToFile('source.txt');
            lboxDest.Items.SaveToFile('dest.txt');
          end;
          Inc(Loop, Index - Loop);
        end;
        Inc(Loop);
      end;
    end;    // while
    statusbar.panels[0].text := 'Comparing Files';
    pbar.Max := lboxSource.Items.Count;
    pbar.Step := 1;
    Loop := 0;
    while (Loop < lboxSource.Items.Count) and (not Cancelled)
    do begin
      Application.ProcessMessages;
      if ((lboxSource.Items[Loop] = lboxDest.Items[Loop]) and (lboxSource.Items[Loop] <> MISSING))
      then begin
        statusbar.panels[0].text := 'Comparing: ' + lboxSource.Items[Loop];
        Application.ProcessMessages;
        if (CompareFiles(lboxSource.Items[Loop],lboxDest.Items[Loop],Loop) = SAME)
        then begin
          // same, remove from the list
          lboxSource.Items.Delete(Loop);
          lboxDest.Items.Delete(Loop);
        end
        else begin
          inc(Loop);
          Inc(Failed);
          statusbar.panels[1].Text := inttostr(Failed);
        end;
      end
      else inc(Loop);
      pbar.StepIt;
    end;    // while
    memoReport.Lines.Add('Comparison Results from: ' + edPath1.Text + ' and ' + edPath2.Text);
    memoReport.Lines.Add('=================================================================');
    memoReport.Lines.Add('');
    for Loop := 0 to lboxSource.Items.Count - 1
    do begin    // Iterate
      if (lboxSource.Items[Loop] = lboxDest.Items[Loop])
      then begin
        memoReport.Lines.Add(lboxSource.Items[Loop] + ' : Different');
        if Assigned(lboxSource.Items.Objects[Loop]) then
          memoReport.Lines.Add(DiffDetails(TStringList(lboxSource.Items.Objects[Loop])));
      end
      else begin
        if (lboxSource.Items[Loop] = MISSING)
        then memoReport.Lines.Add(edPath1.Text + ' is missing file: ' + lboxDest.Items[Loop])
        else memoReport.Lines.Add(edPath2.Text + ' is missing file: ' + lboxSource.Items[Loop])
      end;
    end;    // for
    memoReport.Lines.Add('');
    memoReport.Lines.Add('end of report');
    memoReport.Visible := TRUE;
    memoReport.BringToFront;
  finally
    screen.Cursor := crDefault;
    button1.enabled := true;
    btnCancel.Visible := FALSE;
    btnCancel.Enabled := TRUE;
  end;
end;

procedure TForm1.FormCreate(Sender: TObject);
var iniFile: TIniFile;
begin
  iniFile := TIniFile.Create(ChangeFileExt(ParamStr(0),'.ini'));
  try
    edPath1.Text := iniFile.ReadString('Settings', 'Path1', '');
    edPath2.Text := iniFile.ReadString('Settings', 'Path2', '');
  finally
    FreeAndNil(iniFile);
  end;
end;

procedure TForm1.FormDestroy(Sender: TObject);
var iniFile: TIniFile;
begin
  iniFile := TIniFile.Create(ChangeFileExt(ParamStr(0),'.ini'));
  try
    iniFile.WriteString('Settings', 'Path1', edPath1.Text);
    iniFile.WriteString('Settings', 'Path2', edPath2.Text);
  finally
    FreeAndNil(iniFile);
  end;
end;

function TForm1.PickDir(CurrDir: String): String;
var xDir: String;
begin
  Result := CurrDir;
  xDir := ExtractFilePath(Paramstr(0));
  if FileCtrl.SelectDirectory(xDir, [sdAllowCreate, sdPerformCreate, sdPrompt], 0) then
    Result := xDir;
end;

procedure TForm1.sbPath1Click(Sender: TObject);
begin
  edPath1.Text := PickDir(edPath1.Text);
end;

procedure TForm1.sbPath2Click(Sender: TObject);
begin
  edPath2.Text := PickDir(edPath2.Text);
end;

procedure TForm1.btnCancelClick(Sender: TObject);
begin
  Cancelled := TRUE;
  btnCancel.Enabled := FALSE;
end;

end.
