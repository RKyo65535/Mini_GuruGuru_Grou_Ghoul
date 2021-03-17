public interface IPlayerUseSkill
{
    /// <summary>
    /// スキルを選択できる状態かどうかを返す
    /// </summary>
    /// <returns></returns>
    bool CanSelectSkill();

    /// <summary>
    /// プレイヤーがスキルを発動する際に、これを呼び出す。
    /// </summary>
    bool CanUseSkill();

    /// <summary>
    /// スキルの使用終了の宣言
    /// </summary>
    void EndUseSkill();
}
