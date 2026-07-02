#include <jni.h>
#include <string>
#include <android/log.h>
extern "C" JNIEXPORT jstring JNICALL
Java_com_tgk_parse_NdkAccelerator_translateFast(JNIEnv* env, jobject, jstring input) {
    const char* str = env->GetStringUTFChars(input, nullptr);
    std::string result(str);
    env->ReleaseStringUTFChars(input, str);
    return env->NewStringUTF(result.c_str());
}
