//作成者:杉山
//PhotonのRaiseEventで使うコード(0～255のどれかを被らないように使う)
//注意点:0はChatGPT曰く特別なコードの可能性があるため、0を使うのは非推奨とのこと

public static class EventCodeDictionary
{
    public const byte EVENTCODE_CAPTURECORD = 1;//捕まえた履歴を送信する際に使うコード
}
