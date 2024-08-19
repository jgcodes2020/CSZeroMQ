using System.Runtime.InteropServices;


namespace CSZeroMQ.Native
{
    public unsafe partial struct zmq_msg_t
    {
        [NativeTypeName("unsigned char[64]")]
        public fixed byte _[64];
    }

    public unsafe partial struct unix_zmq_pollitem_t
    {
        public void* socket;

        [NativeTypeName("zmq_fd_t")]
        public int fd;

        public short events;

        public short revents;
    }
    
    public unsafe partial struct win_zmq_pollitem_t
    {
        public void* socket;

        [NativeTypeName("zmq_fd_t")]
        public nuint fd;

        public short events;

        public short revents;
    }

    public partial struct iovec
    {
    }
    
    public unsafe class ZMQ
    {
        static ZMQ()
        {
            // Load the native library when the ZMQ class is accessed for the first time
            NativeLibLoader.LoadLibrary();
        }
        
        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_errno();

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        [return: NativeTypeName("const char *")]
        public static extern byte* zmq_strerror(int errnum_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern void zmq_version(int* major_, int* minor_, int* patch_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern void* zmq_ctx_new();

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_ctx_term(void* context_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_ctx_shutdown(void* context_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_ctx_set(void* context_, int option_, int optval_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_ctx_get(void* context_, int option_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern void* zmq_init(int io_threads_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_term(void* context_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_ctx_destroy(void* context_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_msg_init(zmq_msg_t* msg_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_msg_init_size(zmq_msg_t* msg_, [NativeTypeName("size_t")] nuint size_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_msg_init_data(zmq_msg_t* msg_, void* data_, [NativeTypeName("size_t")] nuint size_, [NativeTypeName("zmq_free_fn *")] delegate* unmanaged[Cdecl]<void*, void*, void> ffn_, void* hint_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_msg_send(zmq_msg_t* msg_, void* s_, int flags_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_msg_recv(zmq_msg_t* msg_, void* s_, int flags_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_msg_close(zmq_msg_t* msg_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_msg_move(zmq_msg_t* dest_, zmq_msg_t* src_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_msg_copy(zmq_msg_t* dest_, zmq_msg_t* src_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern void* zmq_msg_data(zmq_msg_t* msg_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        [return: NativeTypeName("size_t")]
        public static extern nuint zmq_msg_size([NativeTypeName("const zmq_msg_t *")] zmq_msg_t* msg_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_msg_more([NativeTypeName("const zmq_msg_t *")] zmq_msg_t* msg_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_msg_get([NativeTypeName("const zmq_msg_t *")] zmq_msg_t* msg_, int property_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_msg_set(zmq_msg_t* msg_, int property_, int optval_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        [return: NativeTypeName("const char *")]
        public static extern byte* zmq_msg_gets([NativeTypeName("const zmq_msg_t *")] zmq_msg_t* msg_, [NativeTypeName("const char *")] byte* property_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern void* zmq_socket(void* param0, int type_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_close(void* s_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_setsockopt(void* s_, int option_, [NativeTypeName("const void *")] void* optval_, [NativeTypeName("size_t")] nuint optvallen_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_getsockopt(void* s_, int option_, void* optval_, [NativeTypeName("size_t *")] nuint* optvallen_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_bind(void* s_, [NativeTypeName("const char *")] byte* addr_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_connect(void* s_, [NativeTypeName("const char *")] byte* addr_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_unbind(void* s_, [NativeTypeName("const char *")] byte* addr_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_disconnect(void* s_, [NativeTypeName("const char *")] byte* addr_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_send(void* s_, [NativeTypeName("const void *")] void* buf_, [NativeTypeName("size_t")] nuint len_, int flags_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_send_const(void* s_, [NativeTypeName("const void *")] void* buf_, [NativeTypeName("size_t")] nuint len_, int flags_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_recv(void* s_, void* buf_, [NativeTypeName("size_t")] nuint len_, int flags_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_socket_monitor(void* s_, [NativeTypeName("const char *")] byte* addr_, int events_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_poll(unix_zmq_pollitem_t* items_, int nitems_, [NativeTypeName("long")] nint timeout_);
        
        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_poll(win_zmq_pollitem_t* items_, int nitems_, [NativeTypeName("long")] nint timeout_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_proxy(void* frontend_, void* backend_, void* capture_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_proxy_steerable(void* frontend_, void* backend_, void* capture_, void* control_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_has([NativeTypeName("const char *")] byte* capability_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_device(int type_, void* frontend_, void* backend_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_sendmsg(void* s_, zmq_msg_t* msg_, int flags_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_recvmsg(void* s_, zmq_msg_t* msg_, int flags_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_sendiov(void* s_, [NativeTypeName("struct iovec *")] iovec* iov_, [NativeTypeName("size_t")] nuint count_, int flags_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_recviov(void* s_, [NativeTypeName("struct iovec *")] iovec* iov_, [NativeTypeName("size_t *")] nuint* count_, int flags_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        [return: NativeTypeName("char *")]
        public static extern byte* zmq_z85_encode([NativeTypeName("char *")] byte* dest_, [NativeTypeName("const uint8_t *")] byte* data_, [NativeTypeName("size_t")] nuint size_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        [return: NativeTypeName("uint8_t *")]
        public static extern byte* zmq_z85_decode([NativeTypeName("uint8_t *")] byte* dest_, [NativeTypeName("const char *")] byte* string_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_curve_keypair([NativeTypeName("char *")] byte* z85_public_key_, [NativeTypeName("char *")] byte* z85_secret_key_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_curve_public([NativeTypeName("char *")] byte* z85_public_key_, [NativeTypeName("const char *")] byte* z85_secret_key_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern void* zmq_atomic_counter_new();

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern void zmq_atomic_counter_set(void* counter_, int value_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_atomic_counter_inc(void* counter_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_atomic_counter_dec(void* counter_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_atomic_counter_value(void* counter_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern void zmq_atomic_counter_destroy(void** counter_p_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern void* zmq_timers_new();

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_timers_destroy(void** timers_p);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_timers_add(void* timers, [NativeTypeName("size_t")] nuint interval, [NativeTypeName("zmq_timer_fn")] delegate* unmanaged[Cdecl]<int, void*, void> handler, void* arg);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_timers_cancel(void* timers, int timer_id);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_timers_set_interval(void* timers, int timer_id, [NativeTypeName("size_t")] nuint interval);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_timers_reset(void* timers, int timer_id);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        [return: NativeTypeName("long")]
        public static extern nint zmq_timers_timeout(void* timers);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern int zmq_timers_execute(void* timers);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern void* zmq_stopwatch_start();

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern nuint zmq_stopwatch_intermediate(void* watch_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        [return: NativeTypeName("unsigned long")]
        public static extern nuint zmq_stopwatch_stop(void* watch_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern void zmq_sleep(int seconds_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern void* zmq_threadstart([NativeTypeName("zmq_thread_fn *")] delegate* unmanaged[Cdecl]<void*, void> func_, void* arg_);

        [DllImport("libzmq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = true)]
        public static extern void zmq_threadclose(void* thread_);
    }
}
