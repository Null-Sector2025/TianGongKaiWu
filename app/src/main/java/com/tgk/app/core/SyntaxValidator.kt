package com.tgk.app.core

object SyntaxValidator {
    fun validate(code: String): Pair<Boolean, String> {
        if (code.isBlank()) return false to "代码为空"
        return true to ""
    }

    fun containsNdk(code: String): Boolean {
        return code.contains("daoru_ndk") || code.contains("#include <jni.h>")
    }
}
