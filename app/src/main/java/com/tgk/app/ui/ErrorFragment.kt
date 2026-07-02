package com.tgk.app.ui

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.tgk.app.core.ErrorLogger
import com.tgk.app.databinding.FragmentErrorBinding

class ErrorFragment : Fragment() {
    private var _binding: FragmentErrorBinding? = null
    private val binding get() = _binding!!

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        _binding = FragmentErrorBinding.inflate(inflater, container, false)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        val sb = StringBuilder()
        ErrorLogger.Errors.forEach { sb.append("${it.ShortInfo}\n${it.StackTrace}\n\n") }
        binding.textErrors.text = sb.ifEmpty { "暂无错误记录" }
        binding.btnClear.setOnClickListener {
            ErrorLogger.clear()
            binding.textErrors.text = "暂无错误记录"
        }
    }

    override fun onDestroyView() { super.onDestroyView(); _binding = null }
}
