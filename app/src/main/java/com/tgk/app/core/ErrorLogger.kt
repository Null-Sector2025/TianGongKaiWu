package com.tgk.app.core

object ErrorLogger {
    private val _errors = mutableListOf<ErrorRecord>()

    val Errors: List<ErrorRecord> get() = _errors.toList()

    fun log(context: String, message: String, stackTrace: String = "") {
        _errors.add(ErrorRecord(System.currentTimeMillis(), context, message, stackTrace))
        if (_errors.size > 100) _errors.removeAt(0)
    }

    fun clear() = _errors.clear()
}

data class ErrorRecord(
    val time: Long,
    val context: String,
    val message: String,
    val stackTrace: String
) {
    val ShortInfo: String get() = "[${java.text.SimpleDateFormat("HH:mm:ss", java.util.Locale.getDefault()).format(java.util.Date(time))}] $context: $message"
}
