using UnityEngine;

/// <summary>
/// 유저 데이터 저장/로드에 사용되는 클래스입니다.
/// </summary>
public class SaveModule
{
    private string _key = "Score";

    /// <summary>
    /// 객체 생성 시 사용될 생성자
    /// </summary>
    /// <param name="key">키 문자열</param>
    public SaveModule(in string key) => _key = key;

   /// <summary>
   /// UserData 저장
   /// </summary>
   /// <param name="userData">저장할 UserData</param>
    public void Save(UserData userData)
    {
        string user = JsonUtility.ToJson(userData);
        PlayerPrefs.SetString(_key, user);
        PlayerPrefs.Save();
    }

   /// <summary>
   /// UserData 불러오기
   /// </summary>
   /// <returns></returns>
    public UserData Load()
    {
        UserData userData;
        
        if (PlayerPrefs.HasKey(_key))
        {
            string user = PlayerPrefs.GetString(_key);
            userData = JsonUtility.FromJson<UserData>(user);
        }
        else
        {
            userData = new UserData();
        }

        return userData;
    }
    
   /// <summary>
   /// 해당 키에 대한 데이터 지우기
   /// </summary>
   /// <param name="key">키 문자열</param>
    public void DeleteData(in string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
}