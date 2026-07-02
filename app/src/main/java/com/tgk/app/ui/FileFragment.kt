package com.tgk.app.ui

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.fragment.app.Fragment
import com.google.android.material.dialog.MaterialAlertDialogBuilder
import com.tgk.app.core.ProjectManager
import com.tgk.app.databinding.FragmentFileBinding

class FileFragment : Fragment() {
    private var _binding: FragmentFileBinding? = null
    private val binding get() = _binding!!

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        _binding = FragmentFileBinding.inflate(inflater, container, false)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        refreshList()
        binding.btnNew.setOnClickListener { newProject() }
        binding.btnDelete.setOnClickListener { deleteProject() }
    }

    private fun refreshList() {
        val files = ProjectManager.getProjects()
        val names = files.map { it.name }
        val adapter = ArrayAdapter(requireContext(), android.R.layout.simple_list_item_1, names)
        binding.listProjects.adapter = adapter
    }

    private fun newProject() {
        val name = binding.editName.text.toString()
        if (name.isBlank()) { Toast.makeText(requireContext(), "输入名称", Toast.LENGTH_SHORT).show(); return }
        ProjectManager.createProject("$name.pinyin")
        refreshList()
        binding.editName.text?.clear()
    }

    private fun deleteProject() {
        val files = ProjectManager.getProjects()
        if (files.isEmpty()) { Toast.makeText(requireContext(), "没有项目", Toast.LENGTH_SHORT).show(); return }
        val names = files.map { it.name }.toTypedArray()
        MaterialAlertDialogBuilder(requireContext())
            .setTitle("删除项目")
            .setItems(names) { _, which ->
                files[which].delete()
                refreshList()
                Toast.makeText(requireContext(), "已删除", Toast.LENGTH_SHORT).show()
            }.show()
    }

    override fun onDestroyView() { super.onDestroyView(); _binding = null }
}
