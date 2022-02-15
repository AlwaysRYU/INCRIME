using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace User
{
    //User ��ü�� ��Ƽ� JsonSerialize�ϱ� ���� Ŭ����
    [Serializable]
    public class UsersWrapper
    {
        public User[] users;
    }

    //public �̹Ƿ� user_pw�� ���� ���� �� ���� �Ͻ������� ����ϰ� �ı��� ��
    [Serializable]
    public class User
    {
        public string user_id;
        public int enter_no;
        public string user_name;
        public string user_pw;
        public string user_email;
    }
}
