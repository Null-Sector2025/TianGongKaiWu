package com.tgk.app.core

object PinyinTranslator {
    fun translate(code: String, lang: String): String {
        var result = code
        GrammarManager.grammarMap.forEach { (pinyin, target) ->
            val replacement = if (target.contains(" ")) target.split(" ")[0] else target
            result = result.replace(pinyin, replacement)
        }
        return when (lang) {
            "python" -> result.replace("zhen", "True").replace("jia", "False")
            "javascript" -> result.replace("dayin", "console.log")
            else -> result
        }
    }
}
