//捕まえた履歴情報
//捕まったターン数、捕まった人(1人だけ)、捕まえた人(複数人の可能性あり)の情報を格納
//捕まった人、捕まえた人はActorNumerで識別

public class CaptureRecord
{
    int _captureTurn;//捕まったターン数
    int _caughtRunnerActorNum;//捕まった人のactorNumber
    int[] _caughtTaggerActorNum;//捕まえた人のactorNumber

    public CaptureRecord(int captureTurn, int caughtRunnerActorNum, int[] caughtTaggerActorNum)
    {
        _captureTurn = captureTurn;
        _caughtRunnerActorNum = caughtRunnerActorNum;
        _caughtTaggerActorNum = caughtTaggerActorNum;
    }

    public int CaptureTurn { get { return _captureTurn; } }

    public int CaughtRunnerActorNum {  get { return _caughtRunnerActorNum; }}

    public int[] CaughtTaggerActorNum { get {return _caughtTaggerActorNum; }}

}
