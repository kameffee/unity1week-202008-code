using UnityEngine;

public class GameObjectExtensions
{
    /// <summary>
    /// 指定されたインターフェイスを実装したコンポーネントを持つオブジェクトを検索します
    /// </summary>
    public static T FindObjectOfInterface<T>() where T : class
    {
        foreach ( var n in GameObject.FindObjectsOfType<Component>() )
        {
            var component = n as T;
            if ( component != null )
            {
                return component;
            }
        }
        return null;
    }
}