using System;

namespace TryXUnit
{

    public class ToBeTested
    {
        MyInterface _injected;
        public ToBeTested(MyInterface injected)
        {
            _injected = injected;
        }

        public string ToBeTestedMethod(int id){
            if (id<1){
                throw new ArgumentException("Invalid Id");
            }
            return _injected.ReturnsName(id);
        }
    }

    public interface MyInterface{
        string ReturnsName(int id);
    }

    public class MyClass : MyInterface
    {
        public string ReturnsName(int id)
        {
            throw new NotImplementedException();
        }
    }
}
