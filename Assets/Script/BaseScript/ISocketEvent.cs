public interface ISocketEvent
{
    void ISocketEngineSink();
    void OnEventTCPSocketLink();
    void OnEventTCPSocketShut();
    void OnEventTCPSocketError(int errorCode);
    bool OnEventTCPSocketRead(int main, int sub, byte[] tmpBuf, int size);
    bool OnEventTCPHeartTick();
}